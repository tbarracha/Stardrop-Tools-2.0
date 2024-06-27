using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardropTools
{
    public interface ILevelManager
    {
        public void GenerateLevel();
        public void PauseLevel();
        public void ResumeLevel();
        public void LevelWon();
        public void LevelLost();
        public void NextLevel();
        public void SaveLevel();
    }
}
