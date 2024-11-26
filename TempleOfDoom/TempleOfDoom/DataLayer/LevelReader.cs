using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.DataLayer.DTO;
using TempleOfDoom.Interfaces;

namespace TempleOfDoom.DataLayer
{
    public class LevelReader : ILevelDataReader
    {
        private RootDTO? RootDTO {  get; set; }
        public LevelReader(string path)
        {
            RootDTO = readFile(path);
        }
        public RootDTO readFile(string path)
        {
            string? extension = Path.GetExtension(path)?.ToLower()?.TrimStart('.');
            if (string.IsNullOrEmpty(extension)) throw new Exception("invalid path");
            // determine reader type through reflection
            string classname = "TempleOfDoom.DataLayer.ReaderStrategies." + char.ToUpper(extension[0]) + extension.Substring(1) + "LevelDataReader";
            // check if type exists
            Type? readerType = Type.GetType(classname);
            if (readerType == null) throw new Exception(extension + " is not supported");
            // create instance if exists
            ILevelDataReader? levelDataReader = Activator.CreateInstance(readerType) as ILevelDataReader;
            return levelDataReader.readFile(path);
        }
    }
}
