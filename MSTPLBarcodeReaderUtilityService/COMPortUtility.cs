using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Configuration;
using System.Threading;
using Domain;
using Persistence;
using System.Data.SqlClient;
using NLog;

namespace MSTPLBarcodeReaderUtilityService
{
    public class COMPortUtility
    {
        private static Logger logger = LogManager.GetCurrentClassLogger(typeof(COMPortUtility));
        private string[] COMPortNames;
        private int SerialPortBaudRate;
        private int SerialPortReadTimeOut;
        private Parity SerialPortParity;
        private int SerialPortDataBits;
        private StopBits SerialPortStopBits;
        private Handshake SerialPortHandshake;
        private Mutex SyncLock;
        private Thread[] COMPortReaderThreads;
        private BarcodeTransactionDAO btdao;
        private string DBServerConnectionString;
        private BarcodeReaderDAO brdao;

        public COMPortUtility()
        {
            logger.Info("Barcode reading service started successfully.");

            COMPortNames = SerialPort.GetPortNames();
            if (!Int32.TryParse(ConfigurationManager.AppSettings.Get("SerialPortBaudRate"), out SerialPortBaudRate))
                SerialPortBaudRate = 9600;

            if (!Int32.TryParse(ConfigurationManager.AppSettings.Get("SerialPortReadTimeout"), out SerialPortReadTimeOut))
                SerialPortReadTimeOut = 500;

            if (!Enum.TryParse<Parity>(ConfigurationManager.AppSettings.Get("SerialPortParity"), out SerialPortParity))
                SerialPortParity = Parity.None;

            if (!Int32.TryParse(ConfigurationManager.AppSettings.Get("SerialDataBits"), out SerialPortDataBits))
                SerialPortDataBits = 8;

            if (!Enum.TryParse<StopBits>(ConfigurationManager.AppSettings.Get("SerialPortStopBits"), out SerialPortStopBits))
                SerialPortStopBits = StopBits.One;

            if (!Enum.TryParse<Handshake>(ConfigurationManager.AppSettings.Get("SerialPortHandShake"), out SerialPortHandshake))
                SerialPortHandshake = Handshake.None;

            SyncLock = new Mutex(false, "COMPortDataReadMutex");
            COMPortReaderThreads = new Thread[COMPortNames.Length];

            int i = 0;

            foreach (string COMPortName in COMPortNames)
            {
                ParameterizedThreadStart pts = new ParameterizedThreadStart(InitializeCOMPort);
                COMPortReaderThreads[i] = new Thread(pts);
                COMPortReaderThreads[i].Start(COMPortName);
                i++;
            }

            foreach (Thread t in COMPortReaderThreads)
                t.Join();

            DBServerConnectionString = ConfigurationManager.ConnectionStrings["DBServerConnectionString"].ToString();
            btdao = new BarcodeTransactionDAO(DBServerConnectionString);
            brdao = new BarcodeReaderDAO(DBServerConnectionString);
        }

        private void InitializeCOMPort(object COMPortName)
        {
            SerialPort port = new SerialPort();

            try
            {
                port.PortName = COMPortName.ToString();
                port.BaudRate = SerialPortBaudRate;
                port.ReadTimeout = SerialPortReadTimeOut;
                port.Parity = SerialPortParity;
                port.DataBits = SerialPortDataBits;
                port.StopBits = SerialPortStopBits;
                port.Handshake = SerialPortHandshake;

                if (!port.IsOpen)
                    port.Open();

                port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
                logger.Info("Readers detected and opened on port: " + port.PortName);
            }
            catch (UnauthorizedAccessException uae)
            {
                logger.Error("Can not access " + port.PortName + ": " + uae.Message.ToString());
            }
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs s)
        {
            try
            {
                SyncLock.WaitOne();
                BarcodeTransaction bt = new BarcodeTransaction();
                SerialPort sp = (SerialPort)sender;
                string ReadData = sp.ReadExisting();
                string ModSerNum = ReadData.Substring(0, 14);
                int ReaderID = 1;

                //if (!Int32.TryParse(ReadData.Substring(15, 2), out ReaderID))
                //    throw new FormatException("Can not parse Barcode Reader ID" + ReadData.Substring(15, 2).ToString());

                bt.ModuleSerialNumber = ModSerNum;
                bt.LaminatorBarcodeReaderMappingID = brdao.GetBarcodeReaderLaminatorMappingIDByBarcodeReaderID(ReaderID);
                bt.CreationTime = System.DateTime.Now;
                btdao.InsertNewBarcodeReaderTransaction(bt);
                SyncLock.ReleaseMutex();
            }
            catch (FormatException fe)
            {
                logger.Error(fe.Message.ToString());
            }
            catch (SqlException se)
            {
                logger.Error(se.Message.ToString());
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message.ToString());
            }
        }
    }
}
