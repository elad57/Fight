using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Fight
{
    public delegate void OnConnectionHandler();

    abstract class OnlineGame
    {
        protected BinaryReader reader;
        protected BinaryWriter writer;

        protected Thread thread;

        protected TcpClient client;

        protected int port;

        public Fighter hostChar, joinChar;

        public event OnConnectionHandler OnConnection;

        protected void RaiseOnConnectionEvent()
        {
            if (OnConnection != null)
                OnConnection();
        }

        public void Init()
        {
            InitChars();
            StartCommunication();
        }

        protected abstract void InitChars();

        public void StartCommunication()
        {
            thread = new Thread(new ThreadStart(SocketThread));
            thread.IsBackground = true;
            thread.Start();
        }

        protected void ReadAndUpdateCharacter(Fighter c)
        {
            c.Pos.X = reader.ReadSingle();
            c.Pos.Y = reader.ReadSingle();
            c.Rot = reader.ReadSingle();
        }

        protected void WriteCharacterData(Fighter c)
        {
            writer.Write(c.Pos.X);
            writer.Write(c.Pos.Y);
            writer.Write(c.Rot);
        }

        protected abstract void SocketThread();

    }
    class HostOnlineGame : OnlineGame
    {

        public HostOnlineGame(int port)
        {
            this.port = port;
        }

        protected override void InitChars()
        {
            hostChar = G.btl.f1;

            joinChar = G.btl.f2;
        }

        protected override void SocketThread()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            client = listener.AcceptTcpClient();

            reader = new BinaryReader(client.GetStream());
            writer = new BinaryWriter(client.GetStream());

            base.RaiseOnConnectionEvent();


            while (true)
            {
                WriteCharacterData(hostChar);
                ReadAndUpdateCharacter(joinChar);

                Thread.Sleep(10);
            }

        }
    }
    class JoinOnlineGame : OnlineGame
    {
        string hostip;

        public JoinOnlineGame(string hostip, int port)
        {
            this.port = port;
            this.hostip = hostip;
        }

        protected override void InitChars()
        {
            //hostChar = new Fighter(Folders.ryu, G.keys1, new Vector2(800, 700));

            //joinChar = new Fighter(Folders.ryu, G.keys1, new Vector2(1200, 700));
        }

        protected override void SocketThread()
        {
            client = new TcpClient();
            client.Connect(hostip, port);

            reader = new BinaryReader(client.GetStream());
            writer = new BinaryWriter(client.GetStream());

            base.RaiseOnConnectionEvent();

            while (true)
            {
                ReadAndUpdateCharacter(hostChar);
                WriteCharacterData(joinChar);

                Thread.Sleep(10);
            }
        }
    }
}

