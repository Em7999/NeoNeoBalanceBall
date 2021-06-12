using UnityEngine;
using System.Collections;
using System.IO;
using System.Threading;
using System.IO.Ports;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
//此代码的数据例：（53以后的就是陀螺仪的数据）
//55 51 F0 FF 18 00 FE 07 3E FA EA 55 52 00 00 00 00 00 00 3E FA DF 55 53 83 00 49 00 73 C1 3E FA E0

//使用说明：
//①请自行设置串口号，波特率等
//②请注意要设置好数据长度

public class ReceiveProcessP1 : MonoBehaviour
{

    //************************串口属性
    string portName = "COM3";
    int baudRate = 115200;
    Parity parity = Parity.None;
    int dataBits = 8;
    StopBits stopBits = StopBits.One;
    SerialPort sp = null; //定义一个串口
    //************************

    //接收极其重要的部分，必须规定好一次接收的数据长度！！！！！！！！！！！！！！！！！！！！！！！！！！
    //自行设置每次接收的数据长度，若数据长度不固定，请自己想办法（例如读到某标志位为止）
    //接收极其重要的部分，必须规定好一次接收的数据长度！！！！！！！！！！！！！！！！！！！！！！！！！！
    Mutex mtu;
    [Header("旋转物体")]
    public PlayerController1 _p1;

    public string PortName;
    bool bopen;  //串口是否打开
    byte[] mdata;  //数据
    int pos1;  //数据开始
    int pos2;  //数据结束
    Coroutine mco1;
    Coroutine mco2;
    int msize;  //缓冲区长度

    Quaternion iqua;  //
    Quaternion lqua;

    void Start()
    {



        msize = 40960;
        mtu = new Mutex(false);
        mdata = new byte[msize];//存储数据
        pos1 = 0;
        pos2 = 0;
    }

    private void Update()
    {


    }
    public void onbtn()
    {

        if (sp != null && bopen)
        {
            if (mco1 != null)
            {
                StopCoroutine(mco1);  //停止数据接收
            }
            if (mco2 != null)
            {
                StopCoroutine(mco2);   //停止数据处理
            }
            ClosePort();

        }
        else
        {
            OpenPort();//打开串口   
        }

    }

    //打开串口
    public void OpenPort()
    {

        bopen = false;
        // portName = coms.options[coms.value].text;
        portName = PortName;
        sp = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
        Debug.Log(portName);
        sp.ReadTimeout = 400;
        sp.ReadBufferSize = 81920;
        try
        {
            sp.Open();  //打开串口
            if (sp.IsOpen)
            {
                bopen = true;
                iqua = Quaternion.identity;
                lqua = Quaternion.identity;



                pos1 = 0;
                pos2 = 0;
                mco1 = StartCoroutine(DataReceiveFunction());//打开接收数据的协程
                mco2 = StartCoroutine(Process());

                Debug.LogWarning(name + " openport");
            }
            else
            {
                bopen = false;

            }

        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);

        }
    }

    //关闭串口
    public void ClosePort()
    {
        try
        {
            sp.Close();
            sp = null;
            Debug.LogWarning(name + " 关闭串口");
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    //解析角度（基于维特智能的陀螺仪数据）
    void setrangle(Vector3 v)  //角度
    {
        lqua = Quaternion.Euler(v.y, -v.z, v.x);
        Vector3 ve = lqua.eulerAngles - iqua.eulerAngles;  //相对上次旋转
        Vector3 ve1 = new Vector3(v.x, v.y, v.z);

            _p1.setangle(-v);  //旋转物体


    }




    int DecodeIMUData()
    {
        float T = 0;
        float[] marr = new float[3];
        int r = 0;
        switch (mdata[pos1])
        {
            case 0x55:  //角度
                if (mdata[pos1 + 1] == 0x61)  //标志位
                {
                    marr[0] = ((short)(mdata[pos1 + 15] << 8 | mdata[pos1 + 14])) / 32768.0f * 180;
                    marr[1] = ((short)(mdata[pos1 + 17] << 8 | mdata[pos1 + 16])) / 32768.0f * 180;
                    marr[2] = ((short)(mdata[pos1 + 19] << 8 | mdata[pos1 + 18])) / 32768.0f * 180;
                    setrangle(new Vector3(marr[0], marr[1], marr[2])); //接收角度
                }
                else
                {
                }

                break;
        }
        pos1 += 20;  //更新数据开始
                     //cs = getstr(a);
                     //Debug.Log("a " + cs);
                     //cs = getstr(w);
                     // Debug.Log("w " + cs);
                     // cs = getstr(Angle);
                     //Debug.Log("Angle " + cs);
        //Debug.Log("T " + T.ToString());
        //  txt.text = "温度：" + cs;
        return r;
    }


    IEnumerator Process()  //数据处理
    {
        int len = 0;
        while (true)
        {
            while (pos1 >= pos2)  //无数据
            {
                yield return null;
               // Debug.Log(name + "....nodata");
            }
            len = pos2 - pos1;
            if (len > 40)  //取最后角度
            {
                pos1 = pos2 - 40;
            }
            mtu.WaitOne();
            while (mdata[pos1] != 0x55)  //非数据开始
            {
                //Debug.Log(name + ".....errdata");
                pos1++;
                if (pos1 >= pos2)
                {
                    break;
                }
            }
            while ((len = pos2 - pos1) >= 20)
            {
                //ck = getsum();  
                //if(ck!=mdata[pos1+10])  //校验和错误
                //{
                //    Debug.LogWarning(ck.ToString()+" "+ mdata[pos1 + 10].ToString()+" cerr");
                //    pos1 += 1;
                //    continue;
                //}
                DecodeIMUData();
                //txt.text = len.ToString();
            }
            mtu.ReleaseMutex();
            yield return null;
        }

    }
    //协程接收温度数据的函数
    IEnumerator DataReceiveFunction()
    {
        bool br;
        while (true)
        {
            int len = sp.BytesToRead;  //待读数据
            while (len <= 0)  //无数据
            {
                len = sp.BytesToRead;
                yield return null;
            }
            //txt.text = len.ToString()+" "+pnum.ToString();
            br = false;
            if (sp != null && sp.IsOpen)
            {
                try
                {
                    //采用单个读取的方法，为了防止读取出现错误采用以下自动匹配的方法。
                    //sp.Read和sp.Readline均会有各种花里胡哨的BUG，无法解决的。
                    if (pos2 + len >= msize)  //超出边界
                    {
                        int n = pos2 - pos1;  //数据长度
                        byte[] data = new byte[msize];
                        Array.Copy(mdata, pos1, data, 0, n);  //移动数据至开始
                        mdata = data;
                        Debug.LogWarning("pos2:" + pos2.ToString() + " " + len.ToString());
                        pos1 = 0;  // 数据位置
                        pos2 = n;
                        GC.Collect();
                    }
                    mtu.WaitOne();
                    int rlen = sp.Read(mdata, pos2, len);

                    pos2 = pos2 + rlen;  //数据结束更新
                    mtu.ReleaseMutex();
                    br = true;
                    //Debug.LogWarning(len.ToString() + " " + rlen.ToString());
                    //Debug.Log("内容是：" + System.Text.Encoding.Default.GetString(dataBytes)); //若接收的数据是字符串，这句可以打印出来
                    //getxyz(dataBytes);//解析（数据格式源于维特智能）
                }
                catch (Exception ex)
                {
                    if (!br)
                    {
                        mtu.ReleaseMutex();
                    }
                    Debug.LogWarning(ex.Message);
                }
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    byte getsum()  //校验和
    {
        int s = 0;
        if (pos1 + 10 >= msize)
        {
            return 0;
        }
        for (int i = pos1; i < pos1 + 10; i++)
        {
            s = s + mdata[i];
        }
        return (byte)s;
    }
    //发送数据
    void WriteData(string stm)
    {
        if (sp.IsOpen)
        {
            sp.Write(stm);
        }
    }

    //在结束程序时关闭串口
    void OnApplicationQuit()
    {
        ClosePort();
    }

}