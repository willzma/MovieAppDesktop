﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kumquat.NET
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                          (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void studioButton4_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void createprof_Click(object sender, EventArgs e)
        {
            createprof.Visible = false;
            noprof.Visible = false;
            userprof.Visible = true;
            profpic.Visible = true;
            profname.Visible = true;
            profemail.Visible = true;
            mymajor.Visible = true;
            mydescription.Visible = true;
            profmajor.Visible = true;
            profdesc.Visible = true;
        }

        private void doSearch2(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                doSearchAct();
            }
        }
        private void doSearch(object sender, EventArgs e)
        {
            doSearchAct();
        }
        private void doSearchAct() { 
                using (WebClient client = new WebClient())
                {
                    String q = searchbox.Text;
                    if (!q.Equals(""))
                    {
                        String htmlCode = client.DownloadString("http://www.omdbapi.com/?s=" + q);
                    if (htmlCode.Contains("Error\":\"Movie")) {
                        MessageBox.Show("Movie was not found.");
                    }
                    else if (htmlCode.Contains("Error\":"))
                    {
                        MessageBox.Show("Search terms must be at least 2 characters long.");
                    } else
                    {
                        listView1.Items.Clear();
                        backgroundWorker1.RunWorkerAsync();
                        List<String> titles = Utils.getAspect(htmlCode, "Title");
                        List<String> years = Utils.getAspect(htmlCode, "Year");
                        List<String> posters = Utils.getAspect(htmlCode, "Poster");
                        for (int i = 0; i < titles.Count; i++)
                        {
                            ListViewItem lvi = new ListViewItem();
                            try {
                                WebRequest requestPic = WebRequest.Create(posters[i]);
                                WebResponse responsePic = requestPic.GetResponse();
                                Image webImage = Image.FromStream(responsePic.GetResponseStream());
                                imageList1.Images.Add(posters[i], webImage);
                                lvi.ImageKey = posters[i];
                            }
                            catch
                            {
                                lvi.ImageKey = "notfound.png";
                            }
                            lvi.Text = titles[i] + " (" + years[i] + ")";
                            listView1.Items.Add(lvi);
                        }
                    }
                }
                }        
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ListViewItem abg = new ListViewItem("cowbell");


            if (listView1.InvokeRequired)
                listView1.Invoke(new MethodInvoker(delegate
                {
                    listView1.Items.Add(abg);

                }));
            else
                listView1.Items.Add(abg);
        }
    }
}
