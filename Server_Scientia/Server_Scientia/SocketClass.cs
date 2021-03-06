﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server_Scientia
{
    class SocketClass
    {
        Socket _mySocket;
        long _uniqueIdx; // 소켓의 인덱스

        public long _UUID { get { return _uniqueIdx; } }

        public SocketClass(Socket socket)
        {
            _mySocket = socket;
        }

        public void ConnectSocket(long uniqueIdx)
        {
            _uniqueIdx = uniqueIdx;
        }

        public void SendBuffer(byte[] buffer)
        {
            if (_mySocket != null)
                _mySocket.Send(buffer);
        }

        public bool ReceiveBuffer(out byte[] buffer, out int recvLen)
        {
            buffer = new byte[1032];
            recvLen = 0;
            
            if (_mySocket != null && _mySocket.Poll(0, SelectMode.SelectRead))
            {
                try
                {
                    recvLen = _mySocket.Receive(buffer);
                    if (recvLen > 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }

            return false;
        }

        public void Close()
        {
            try
            {
                _mySocket.Shutdown(SocketShutdown.Both);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                _mySocket.Close(0);
                _mySocket = null;

                Console.WriteLine("{0} 유저가 접속을 종료하였습니다.", _uniqueIdx);
            }
        }
    }
}
