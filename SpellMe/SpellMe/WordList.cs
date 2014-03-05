using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellMe
{
    public class WordList
    {
        private Stack<String> buffer = new Stack<String>();

        public void Load(String filePath)
        {
            if (File.Exists(filePath))
            {
                using (var reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        buffer.Push(reader.ReadLine());
                    }
                }
            }
        }

        public String GetNextWord()
        {
            return buffer.Any() ? buffer.Pop() : String.Empty;
        }
        
    }
}
