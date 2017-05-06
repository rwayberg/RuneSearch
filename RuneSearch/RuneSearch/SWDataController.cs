using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace RuneSearch
{
    class SWDataController
    {
        public string FileName { get; private set; }
        private SWData.RootObject optData;
        public List<string> MonsterList;

        public SWDataController(string File)
        {
            this.FileName = File;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string fileString = File.ReadAllText(this.FileName);
                this.optData = JsonConvert.DeserializeObject<SWData.RootObject>(fileString);
                this.MonsterList = GetMonsterList();
            }
            catch (Exception ex)
            {   //TODO - exception
                throw ex;
            }
        }

        public List<SWData.Rune> GetRuneList()
        {
            return optData.runes;
        }

        private List<string> GetMonsterList()
        {
            List<string> monList = new List<string>();
            foreach (SWData.Mon monster in this.optData.mons)
            {
                monList.Add(monster.name);
            }
            return monList;
        }

        public List<SWData.Rune> GetMonsterRunes(string MonsterName)
        {
            if (String.IsNullOrEmpty(MonsterName))
                return new List<SWData.Rune>();

            return this.optData.runes.FindAll(x => x.monster_n == MonsterName);

        }

        public List<SWData.Rune> GetSlotRunes(int SlotIndex)
        {
            if (SlotIndex < 0)
                return new List<SWData.Rune>();
            return this.optData.runes.FindAll(x => x.slot == SlotIndex);
        }
    }
}
