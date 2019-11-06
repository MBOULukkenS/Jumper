using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Data.Base
{
    public class BinarySavable<T> : ISavable where T : BinarySavable<T>
    {
        public void Save(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.CreateNew))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, (T) this);
                stream.Flush();
            }
        }

        public static T Load(string path)
        {
            byte[] fileBytes = File.ReadAllBytes(path);
            using (MemoryStream stream = new MemoryStream(fileBytes))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                stream.Seek(0, SeekOrigin.Begin);

                return (T) formatter.Deserialize(stream);
            }
        }
    }
}