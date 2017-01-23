using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FP_Converter
{
    public partial class Form1 : Form
    {
        //ffmpeg -i input -c:v libx265 -preset medium -crf 28 -c:a aac -b:a 128k output.mp4
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        string tmpfile = Directory.GetCurrentDirectory() + "\\tmp.mp4";
        string recycledir = Directory.GetCurrentDirectory() + "\\ConvertedRecycle";
        string errors = "";
        string reports = "";
        Process process = null;
        bool stop = false;
                

        public Form1()
        {
            AllocConsole();
            InitializeComponent();
            Console.ForegroundColor = ConsoleColor.Cyan;
            //MessageBox.Show(Directory.GetCurrentDirectory());
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            //listBox1.Enabled = false;
            stop = false;
            buttonDelSelected.Enabled = false;
            buttonGo.Enabled = false;
            textBoxRequest.Enabled = false;
            buttonStop.Enabled = true;

            labelErrors.Text = "No errors.";
            labelReports.Text = "No reports.";
            reports = "";
            errors = "";

            for (int i = 0; i < listBox1.Items.Count && !stop; i++)
            {
                String videofile = listBox1.Items[i].ToString();
                Console.WriteLine("---------------------------------------------------------------------------------------");
                Console.WriteLine("       ENCODING  " + i + " OF " + listBox1.Items.Count + "     FILE: " + videofile);
                Console.WriteLine("---------------------------------------------------------------------------------------");
                Console.WriteLine("Removing: " + tmpfile);
                try
                {
                    File.Delete(tmpfile);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error: Cannot delete temp file!");
                    errors += "Error: Cannot delete temp file!   for file = " + videofile + "\n";
                    int numErrors = errors.Split('\n').Length;
                    labelErrors.Text = numErrors + " errors!";
                    continue;
                }
                //"ffmpeg.exe -i \"" + videofile + "\" -c:v libx265 -preset slow -crf 30 -c:a aac -b:a 160k \"" + tmpfile + "\"";
                string request = textBoxRequest.Text;
                if (request.Equals(""))
                {
                    Console.WriteLine("Error: no request field provided!");
                    continue;
                }
                if (!request.Contains("(INFILE)"))
                {
                    Console.WriteLine("Error: no (INFILE) placeholder provided!");
                    continue;
                }
                if (!request.Contains("(OUTFILE)"))
                {
                    Console.WriteLine("Error: no (OUTFILE) placeholder provided!");
                    continue;
                }
                Console.WriteLine("Request OK: " + request);
                request = request.Replace("(INFILE)", videofile);
                request = request.Replace("(OUTFILE)", tmpfile);
                cmd(request);
                if (stop)
                {
                    Console.WriteLine("Stopped!");
                    return;
                }
                bool exists = false;
                long filesize = 0;
                bool success = true;
                String error = "none";
                try
                {
                    exists = File.Exists(tmpfile);
                    filesize = new FileInfo(tmpfile).Length;
                }
                catch (Exception exc)
                {
                    success = false;
                    error = e.ToString();
                }
                if (success && exists && filesize > 1000)
                {
                    Console.WriteLine("File successfully converted: " + videofile); 
                    Console.WriteLine("Wait while file unlocks...");
                    while (IsFileLocked(new FileInfo(videofile)))
                    {
                        Console.WriteLine("Kill FFmpeg...");
                        Process[] proc = Process.GetProcessesByName("ffmpeg.exe");
                        if(proc.Length > 0)
                            proc[0].Kill();
                        Thread.Sleep(1000);
                        Application.DoEvents();
                    }

                    Console.WriteLine("Replacing original file...");
                    try
                    {
                        Console.WriteLine("Moving: " + videofile);
                        Console.WriteLine("To: " + recycledir);
                        Directory.CreateDirectory(recycledir);
                        string filerecyclepath = recycledir + Path.DirectorySeparatorChar + Path.GetFileName(videofile);
                        while (File.Exists(filerecyclepath))
                            filerecyclepath += "1";
                        File.Move(videofile, filerecyclepath);
                        Console.WriteLine("Moved.");

                        string path = Path.GetDirectoryName(videofile) + Path.DirectorySeparatorChar;
                        string extension = ".mp4";
                        string filename = Path.GetFileNameWithoutExtension(videofile);
                        string newfilepath = path + filename + extension;
                        Console.WriteLine("Moving: " + tmpfile);
                        Console.WriteLine("To: " + newfilepath);
                        File.Move(tmpfile, newfilepath);
                        Console.WriteLine("Moved.");

                        reports += "Size: " + filesize / 1000 + "kb    File: " + videofile + "\n";
                        int numLines = reports.Split('\n').Length;
                        labelReports.Text = numLines + " reports";

                    }
                    catch(Exception exx){
                        error = exx.ToString();
                        Console.WriteLine("Error moving: " + exx);
                        Console.WriteLine("!!!!!!!!!!!!!!!!!1 ERROR!!!!");
                        Console.WriteLine("success = " + success + "  exists = " + exists + "  size = " + "  errormoving = " + error + "  file = " + videofile);
                        errors += "success = " + success + "  exists = " + exists + "  size = " + "  error = " + error + "  file = " + videofile + "\n";
                        int numErrors = errors.Split('\n').Length;
                        labelErrors.Text = numErrors + " errors!";
                    }
                }
                else
                {
                    Console.WriteLine("!!!!!!!!!!!!!!!!!1 ERROR!!!!");
                    Console.WriteLine("success = " + success + "  exists = " + exists + "  size = " + "  error = " + error + "  file = " + videofile);
                    errors += "success = " + success + "  exists = " + exists + "  size = " + "  error = " + error + "  file = " + videofile + "\n";
                    int numLines = errors.Split('\n').Length;
                    labelErrors.Text = numLines + " errors!";
                }
            }

            //listBox1.Enabled = true;
            buttonDelSelected.Enabled = true;
            buttonGo.Enabled = true;
            textBoxRequest.Enabled = true;
            buttonStop.Enabled = false;
        }



        #region FILE LIST
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            Console.WriteLine("Processing drop...");
            foreach (string file in files)
            {
                if (file.ToLower().EndsWith(".mp4")
                    || file.ToLower().EndsWith(".avi")
                    || file.ToLower().EndsWith(".mov")
                    || file.ToLower().EndsWith(".mpg")
                    || file.ToLower().EndsWith(".3gp")
                    || file.ToLower().EndsWith(".wmv")
                    || file.ToLower().EndsWith(".m4a")
                    || file.ToLower().EndsWith(".mkv"))
                {
                    Console.WriteLine("File added: " + file);
                    listBox1.Items.Add(file);
                }
                else
                {
                    Console.WriteLine("File have unsupported format: " + file);
                }
            }
            Console.WriteLine("Total files: " + listBox1.Items.Count);
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                Console.WriteLine("Detected Drag'n'drop");
                e.Effect = DragDropEffects.Copy;
            }
        }
        private void buttonDelSelected_Click(object sender, EventArgs e)
        {
            while (listBox1.SelectedItem != null)
            {
                Console.WriteLine("Deleted: " + listBox1.SelectedItem);
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
            Console.WriteLine("Total files: " + listBox1.Items.Count);
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem != null) 
            {
                Console.WriteLine("Opening: " + listBox1.SelectedItem);
                System.Diagnostics.Process.Start(listBox1.SelectedItem.ToString());
            }
        }

        #endregion

        #region CMD


        private void cmd(string cmd)
        {
            Console.WriteLine("Executing command: " + cmd);
            process = new Process();
            process.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory() + "\\";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/c " + cmd;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.OutputDataReceived += process_OutputDataReceived;
            process.ErrorDataReceived += process_ErrorDataReceived;

            process.StartInfo.CreateNoWindow = true;
            process.Start();

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            while (process != null && !process.HasExited)
            {
                Application.DoEvents();
                Thread.Sleep(200);
            }
           // process.WaitForExit();
            if(process != null)
                process.Close();
            process = null;
            Console.WriteLine("Command executed.");
        }

        void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.Data);
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(e.Data);
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        #endregion

        private void labelErrors_Click(object sender, EventArgs e)
        {
            MessageBox.Show(errors);
        }

        private void labelReports_Click(object sender, EventArgs e)
        {
            MessageBox.Show(reports);
        }
        protected virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (process != null)
            {
                Console.WriteLine("Stopping...");
                stop = true;
                process.Kill();
                process = null;

                buttonDelSelected.Enabled = true;
                buttonGo.Enabled = true;
                textBoxRequest.Enabled = true;
                buttonStop.Enabled = false;
            }
        }
    }
}
