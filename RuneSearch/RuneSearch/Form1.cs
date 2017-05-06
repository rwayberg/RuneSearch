using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RuneSearch
{
    public partial class frmRuneSearch : Form
    {
        //Sort - https://support.microsoft.com/en-us/help/319401/how-to-sort-a-listview-control-by-a-column-in-visual-c
        SWDataController swDC;
        public frmRuneSearch()
        {
            InitializeComponent();
            swDC = null;
            SetListColumn();
            cbxSlot.Enabled = false;
            btnSearch.Enabled = false;
        }

        private void SetListColumn()
        {
            lvRuneList.View = View.Details;
            lvRuneList.AllowColumnReorder = true;
            lvRuneList.ShowGroups = true;
            lvRuneList.GridLines = true;
            lvRuneList.Columns.Add("Rune Name", 100, HorizontalAlignment.Left);
            lvRuneList.Columns.Add("Set", 100, HorizontalAlignment.Left);
            lvRuneList.Columns.Add("Slot", 100, HorizontalAlignment.Left);
            lvRuneList.Columns.Add("Grade", 100, HorizontalAlignment.Left);
            lvRuneList.Columns.Add("Main Stat", 100, HorizontalAlignment.Left);
            lvRuneList.Columns.Add("Speed", 100, HorizontalAlignment.Left);
            lvRuneList.Columns.Add("HP Percent", 100, HorizontalAlignment.Left);
            lvRuneList.Columns.Add("HP Flat", 100, HorizontalAlignment.Left);
            lvRuneList.Columns.Add("Attack Percent", 100, HorizontalAlignment.Left);
            lvRuneList.Columns.Add("Attack Flat", 100, HorizontalAlignment.Left);
            lvRuneList.Columns.Add("Crit Rate", 100, HorizontalAlignment.Left);
            lvRuneList.Columns.Add("Crit Damage", 100, HorizontalAlignment.Left);
            lvRuneList.Columns.Add("Accuracy", 100, HorizontalAlignment.Left);
            lvRuneList.Columns.Add("Def Percentage", 100, HorizontalAlignment.Left);
            lvRuneList.Columns.Add("Def Flat", 100, HorizontalAlignment.Left);
            lvRuneList.Columns.Add("Resistance", 100, HorizontalAlignment.Left);
        }

        private void frmRuneSearch_Load(object sender, EventArgs e)
        {

        }

        private void loadJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog loaddlg = new OpenFileDialog();
            loaddlg.FileName = "Document";
            loaddlg.DefaultExt = ".json";
            loaddlg.Filter = "JSON documents (.json)|*.json";
            DialogResult result = loaddlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = loaddlg.FileName;
                FileInfo optFI = new FileInfo(file);
                if (!optFI.Exists)
                {
                    DialogResult badFileResult = MessageBox.Show(String.Format("Unable to find file {0}", file), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (badFileResult == DialogResult.OK)
                    {
                        loadJSONToolStripMenuItem_Click(sender, e);
                    }
                }
                swDC = new SWDataController(file);
                cbxSlot.Enabled = true;
                btnSearch.Enabled = true;
                //SetListColumn();
                LoadRunesToListView(swDC.GetRuneList());
                //swDC.
                //if (mainDC.MonsterList.Count > 0)
                //{
                //    cmbMonsterBox.Enabled = true;
                //    cmbMonsterBox.Items.AddRange(mainDC.MonsterList.ToArray());
                //}
            }
        }

        private void LoadRunesToListView(List<SWData.Rune> RuneList)
        {
            foreach(SWData.Rune rune in RuneList)
            {
                ListViewItem lvi = new ListViewItem(new[] { rune.id.ToString(), rune.set, rune.slot.ToString(), rune.grade.ToString(), rune.m_t + " " + rune.m_v.ToString(),
                    rune.sub_spd.ToString(), rune.sub_hpp.ToString(), rune.sub_hpf.ToString(), rune.sub_atkp.ToString(), rune.sub_atkf.ToString(), rune.sub_crate.ToString(),
                    rune.sub_cdmg.ToString(), rune.sub_acc.ToString(), rune.sub_defp.ToString(), rune.sub_deff.ToString(), rune.sub_res.ToString()});
                lvRuneList.Items.Add(lvi);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (swDC == null)
                return;
            if(cbxSlot.SelectedIndex >= 0)
            {
                lvRuneList.Items.Clear();
                LoadRunesToListView(swDC.GetSlotRunes(cbxSlot.SelectedIndex + 1));
            }
        }
    }
}
