using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;

public class SerialHandler : MonoBehaviour
{
	public delegate void SerialDataReceivedEventHandler(TweLiteData data);
	public event SerialDataReceivedEventHandler SerialDataReceived;
	
	public string portName = "/dev/tty.usbserial-AHY1U6SB";
	public int baudRate    = 115200;
	
	private SerialPort serialPort_;
	private bool isRunning_ = false;
	private Queue<string> messageQueue_ = new Queue<string>();
	private Thread thread_;
	private object guard = new object();

	private static SerialHandler instance = null;

	public struct TweLiteData {
		public int id;
		public int target_di;
		public int value;
	}


	public static SerialHandler Instance {
		get { return SerialHandler.instance; }
	}

	void Awake()
	{
		if(instance == null) {
			instance = this;
		} else {
			Destroy( this );
		}
	}
	
	void Update()
	{
		lock(guard) {
			while(messageQueue_.Count > 0) {
				string msg = messageQueue_.Dequeue();
				TweLiteData data;
				data.id = System.Convert.ToInt16(msg.Substring(1, 2), 16);
				data.value = System.Convert.ToInt16(msg.Substring(33, 2), 16);
				data.target_di = System.Convert.ToInt16(msg.Substring(35, 2), 16);
				Debug.Log("id: " + data.id + ", target: " + data.target_di + ", val: " + data.value);
//				SerialDataReceived(data);
			}
		}
	}
	
	void OnDestroy()
	{
		Close();
	}
	
	public void Open()
	{
		try {
			serialPort_ = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
			serialPort_.Open();
			
			isRunning_ = true;
			
			thread_ = new Thread(Read);
			thread_.Start();
			
			Debug.Log("Connected!");
		} catch (System.Exception e) {
			Debug.LogError(e.Message);
		}
	}
	
	public void Close()
	{
		isRunning_ = false;
		
		if (thread_ != null && thread_.IsAlive) {
			thread_.Join();
		}
		
		if (serialPort_ != null && serialPort_.IsOpen) {
			serialPort_.Close();
			serialPort_.Dispose();
		}
	}
	
	private void Read()
	{
		while (isRunning_ && serialPort_ != null && serialPort_.IsOpen) {
			try {
				if (serialPort_.BytesToRead > 0) {
					lock(guard) {
						messageQueue_.Enqueue(serialPort_.ReadLine());
					}
					Thread.Sleep (0);
				}
			} catch (System.Exception e) {
				 Debug.LogWarning(e.Message);
			}
		}
	}
	
	public void Write(string message)
	{
		try {
			serialPort_.Write(message);
		} catch (System.Exception e) {
			 Debug.LogWarning(e.Message);
		}
	}
}