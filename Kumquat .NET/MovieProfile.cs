﻿using Kumquat.NET.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kumquat.NET
{
    public partial class MovieProfile : Form
    {
        Movie mov = null;

        public float myRate = 0;
        public MovieProfile(ListViewItem lvi, String name, Image img, String mUrl)
        {
            InitializeComponent();
            studioTheme1.Text = name;
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                          (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            this.TopMost = true;
            pictureBox1.Image = img;
            String namep = (lvi.Tag.ToString()).Split('☻')[0];
            String datep = (lvi.Tag.ToString()).Split('☻')[1];
            if (datep[0] == ' ')
                datep = datep.Substring(1);
            String title = namep + " " + datep;
            Dictionary<String, Movie> md = DBHelper.getMoviesMap();
            textBox1.Text = (lvi.Tag.ToString()).Split('☻')[0] + (lvi.Tag.ToString()).Split('☻')[1];

            if (md.ContainsKey(title))
            {
                mov = md[title];
                rate.Text = "Rating: " + mov.getAverageRating().ToString() + "/5";
                List<Rating> l = mov.getRatings();
                for (int i = 0; i < l.Count; i++)
                    listView1.Items.Add(l[i].getPoster().getUsername() + " [" + l[i].getRating() + "/5] : " + l[i].getComment());
            } else
            {
                mov = new Movie(title);
                mov.setURL(mUrl);
                DBHelper.addNewMovie(mov);

            }
        }

        private void studioButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void studioTheme1_Click(object sender, EventArgs e)
        {

        }

        private void star1_Click(object sender, EventArgs e)
        {
            if(myRate.Equals(1))
            {
                star1.ForeColor = Color.Transparent;
                star2.ForeColor = Color.Transparent;
                star3.ForeColor = Color.Transparent;
                star4.ForeColor = Color.Transparent;
                star5.ForeColor = Color.Transparent;
                myRate = 0;
                return;
            }
            star1.ForeColor = Color.Yellow;
            star2.ForeColor = Color.Transparent;
            star3.ForeColor = Color.Transparent;
            star4.ForeColor = Color.Transparent;
            star5.ForeColor = Color.Transparent;
            myRate = 1;
        }

        private void star2_Click(object sender, EventArgs e)
        {
            if (myRate.Equals(2))
            {
                star1.ForeColor = Color.Transparent;
                star2.ForeColor = Color.Transparent;
                star3.ForeColor = Color.Transparent;
                star4.ForeColor = Color.Transparent;
                star5.ForeColor = Color.Transparent;
                myRate = 0;
                return;
            }
            star1.ForeColor = Color.Yellow;
            star2.ForeColor = Color.Yellow;
            star3.ForeColor = Color.Transparent;
            star4.ForeColor = Color.Transparent;
            star5.ForeColor = Color.Transparent;
            myRate = 2;
        }

        private void star3_Click(object sender, EventArgs e)
        {
            if (myRate.Equals(3))
            {
                star1.ForeColor = Color.Transparent;
                star2.ForeColor = Color.Transparent;
                star3.ForeColor = Color.Transparent;
                star4.ForeColor = Color.Transparent;
                star5.ForeColor = Color.Transparent;
                myRate = 0;
                return;
            }
            star1.ForeColor = Color.Yellow;
            star2.ForeColor = Color.Yellow;
            star3.ForeColor = Color.Yellow;
            star4.ForeColor = Color.Transparent;
            star5.ForeColor = Color.Transparent;
            myRate = 3;
        }

        private void star4_Click(object sender, EventArgs e)
        {
            if (myRate.Equals(4))
            {
                star1.ForeColor = Color.Transparent;
                star2.ForeColor = Color.Transparent;
                star3.ForeColor = Color.Transparent;
                star4.ForeColor = Color.Transparent;
                star5.ForeColor = Color.Transparent;
                myRate = 0;
                return;
            }
            star1.ForeColor = Color.Yellow;
            star2.ForeColor = Color.Yellow;
            star3.ForeColor = Color.Yellow;
            star4.ForeColor = Color.Yellow;
            star5.ForeColor = Color.Transparent;
            myRate = 4;
        }

        private void star5_Click(object sender, EventArgs e)
        {
            if (myRate.Equals(5))
            {
                star1.ForeColor = Color.Transparent;
                star2.ForeColor = Color.Transparent;
                star3.ForeColor = Color.Transparent;
                star4.ForeColor = Color.Transparent;
                star5.ForeColor = Color.Transparent;
                myRate = 0;
                return;
            }
            star1.ForeColor = Color.Yellow;
            star2.ForeColor = Color.Yellow;
            star3.ForeColor = Color.Yellow;
            star4.ForeColor = Color.Yellow;
            star5.ForeColor = Color.Yellow;
            myRate = 5;
        }

        private void studioButton3_Click(object sender, EventArgs e)
        {
            if(DBHelper.getCurrentUser().getProfile() == null)
            {
                MessageBox.Show("You must create a profile before rating a movie.");
                return;
            }
            Rating r = new Rating(myRate, textBox2.Text, DBHelper.getCurrentUser());
            listView1.Items.Add(DBHelper.getCurrentUser().getUsername() + " [" + myRate + "/5] : " + textBox2.Text);
            DBHelper.addRating(mov, r);
            textBox2.Text = "";
        }
    }
}
