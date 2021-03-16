using System;
using System.IO;
using System.Xml.Serialization;

namespace brady.Helper
{
    public static class XmlToObject
    {
        public static T Convert<T>(string filePath)
        {
            try
            {
                XmlSerializer serializer =
                    new XmlSerializer(typeof(T));
                using (Stream reader = new FileStream(filePath, FileMode.Open))
                {
                    // Call the Deserialize method to restore the object's state.
                    return (T) serializer.Deserialize(reader);
                }
            }
            catch (InvalidOperationException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}