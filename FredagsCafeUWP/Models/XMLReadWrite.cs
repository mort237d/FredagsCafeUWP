using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;

namespace FredagsCafeUWP
{
    internal class XmlReadWrite
    {
        public static async Task<T> ReadObjectFromXmlFileAsync<T>(string filename)
        {
            var serializer = new XmlSerializer(typeof(T));
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.GetFileAsync(filename);
            Stream stream = await file.OpenStreamForReadAsync();
            var objectFromXml = (T)serializer.Deserialize(stream);
            stream.Dispose();
            return objectFromXml;
        }

        public static async Task SaveObjectToXml<T>(T objectToSave, string filename)
        {
            var serializer = new XmlSerializer(typeof(T));
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            Stream stream = await file.OpenStreamForWriteAsync();
            using (stream)
            {
                serializer.Serialize(stream, objectToSave);
            }
        }

        public static async Task SaveAsync<T>(ObservableCollection<T> collection, string fileName)
        {
            Debug.WriteLine("Saving " + fileName + " async...");
            await XmlReadWrite.SaveObjectToXml(collection, fileName + ".xml");
            Debug.WriteLine(fileName + ".count: " + collection.Count);
        }
    }
}

