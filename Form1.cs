using Capture.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace BejeweledBot
{
    public partial class Form1 : Form, IGameInterface
    {
        ProcessCapture _processCapture;
        Board _board;

        public Form1()
        {
            InitializeComponent();

            comboGametype.DataSource = new GametypeSelection[]
            {
                new GametypeSelection{ Type = Gametype.Classic, Name = "Classic" },
                new GametypeSelection{ Type = Gametype.Lightning, Name = "Lightning" },
                new GametypeSelection{ Type = Gametype.Zen, Name = "Zen" },
                new GametypeSelection{ Type = Gametype.Ice, Name = "Ice" }
            };
            comboGametype.DisplayMember = "Name";
            comboGametype.ValueMember = "Type";
        }

        private void btnTakeCapture_Click(object sender, EventArgs e)
        {
            if (_processCapture == null)
            {
                _processCapture = new ProcessCapture();

            }
            
            if (!_processCapture.Initalized)
            {
                _processCapture.Initialize();
                if (!_processCapture.Initalized)
                {
                    MessageBox.Show("Failed to hook into Bejeweled3.exe");
                    return;
                }
            }

            if (_processCapture.Capturing)
            {
                btnTakeCapture.Text = "Start Capture";
                _processCapture.StopCapture();
                btnPlay.Enabled = false;
                comboGametype.Enabled = false;
                checkAutoResetGame.Enabled = false;
                return;
            }

            _processCapture.StartCapture();
            btnTakeCapture.Text = "Stop Capture";

            _board = new Board(this);
            _processCapture.RegisterBitmapCallback(_board.ProcessGametick);
            btnPlay.Enabled = true;
            comboGametype.Enabled = true;
            checkAutoResetGame.Enabled = true;
        }

        const int maxGameLines = 10000;
        int lineCount = 0;
        public void PushGameMessage(string message)
        {
            txtGameLog.Invoke(new MethodInvoker(delegate ()
            {
                if (lineCount > maxGameLines)
                {
                    txtGameLog.Clear();
                    lineCount = 0;
                }
                txtGameLog.AppendText(message);
                txtGameLog.AppendText(Environment.NewLine);
                lineCount += 1;
            }));
        }

        public void UpdateGameImage(Bitmap bitmap)
        {
            picGameImage.Invoke(new MethodInvoker(delegate ()
            {
                if (picGameImage.Image != null)
                {
                    picGameImage.Image.Dispose();
                }
                picGameImage.Image = bitmap;
            }));
        }

        public Point FetchGameWindowCoordinates()
        {
            return new Point(_processCapture.Location.X, _processCapture.Location.Y);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (btnPlay.Text == "Stop")
            {
                btnPlay.Text = "Play";
                comboGametype.Enabled = true;
                btnTakeCapture.Enabled = true;
                checkAutoResetGame.Enabled = true;
                _board.Playing = false;
                return;
            }
            _board.ChangeGametype(((GametypeSelection)comboGametype.SelectedItem).Type);
            _board.AutoResetGame = checkAutoResetGame.Checked;
            comboGametype.Enabled = false;
            btnPlay.Text = "Stop";
            btnTakeCapture.Enabled = false;
            checkAutoResetGame.Enabled = false;
            _board.Playing = true;
        }
    }

    class GametypeSelection
    {
        public Gametype Type { get; set; }
        public string Name { get; set; }
    }

}
