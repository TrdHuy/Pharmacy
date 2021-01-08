using Pharmacy.Base.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Implement.Utils
{
    public class FileIOUtil : BaseFileIOUtil
    {
        public const string USER_IMAGE_FOLDER_NAME = "UserImages";
        public const string MEDICINE_IMAGE_FOLDER_NAME = "MedicineImages";
        public const string CUSTOMER_IMAGE_FOLDER_NAME = "CustomerImages";
        private const long USER_IMAGE_QUALITY = 30;

        public static Bitmap GetBitmapFromName(string imageName, string folderName)
        {
            try
            {
                var directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\" + "Data" + @"\" + folderName;
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                string fileName = imageName + ".jpg";
                string filePath = directory + @"\" + fileName;

                if (File.Exists(filePath))
                {
                    using (var origin = Image.FromFile(filePath))
                    {
                        Bitmap bit = new Bitmap(origin);
                        origin.Dispose();
                        return bit;
                    }
                }
                else
                {
                    return GetDefaultIconBitmap(folderName);
                }
            }
            catch
            {
                return GetDefaultIconBitmap(folderName);
            }
        }

        public static void DeleteImageFile(string fileName, string folderName)
        {
            var directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\" + "Data" + @"\" + folderName;

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string fName = fileName + ".jpg";
            string filePath = directory + @"\" + fName;

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        public static void SaveUserImageFile(string userName, Bitmap userImage)
        {
            var directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\" + "Data" + @"\" + USER_IMAGE_FOLDER_NAME;
            SaveImageToFile(userName, directory, userImage);
        }

        public static void SaveCustomerImageFile(string customerID, Bitmap cusBit)
        {
            var directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\" + "Data" + @"\" + CUSTOMER_IMAGE_FOLDER_NAME;
            SaveImageToFile(customerID, directory, cusBit);
        }

        public static void SaveMedicineImageFile(string medicineID, Bitmap medicineImage)
        {
            var directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\" + "Data" + @"\" + MEDICINE_IMAGE_FOLDER_NAME;
            SaveImageToFile(medicineID, directory, medicineImage);
        }

        private static void SaveImageToFile(string _filename, string _directory, Bitmap _bit)
        {
            if (!Directory.Exists(_directory))
            {
                Directory.CreateDirectory(_directory);
            }
            string fileName = _filename + ".jpg";
            string filePath = _directory + @"\" + fileName;

            // Get an ImageCodecInfo object that represents the JPEG codec.
            ImageCodecInfo myImageCodecInfo = GetEncoderInfo("image/jpeg");

            // Create an Encoder object based on the GUID

            // for the Quality parameter category.
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object.

            // An EncoderParameters object has an array of EncoderParameter

            // objects. In this case, there is only one

            // EncoderParameter object in the array.
            EncoderParameters myEncoderParameters = new EncoderParameters(1);

            // Save the bitmap as a JPEG file with quality
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, USER_IMAGE_QUALITY);
            myEncoderParameters.Param[0] = myEncoderParameter;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            _bit.Save(filePath, myImageCodecInfo, myEncoderParameters);
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        private static Bitmap GetDefaultIconBitmap(string folderName)
        {
            switch (folderName)
            {
                case USER_IMAGE_FOLDER_NAME:
                    return Pharmacy.Properties.Resources.default_user_image;
                case MEDICINE_IMAGE_FOLDER_NAME:
                    return Pharmacy.Properties.Resources.default_medicine_image;
                case CUSTOMER_IMAGE_FOLDER_NAME:
                    return Pharmacy.Properties.Resources.customer_default_icon;
                default: return null;

            }
        }
    }
}
