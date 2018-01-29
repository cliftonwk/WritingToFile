using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
// Add System.IO to use File System, File, DirectoryInfo, FileInfo, FileStream, 
// StreamWriter, StreamReader, BinaryWriter, and BinaryReader
// Add the System.Text library as well if not already

    // May need to run Visual Studio as Administrator for this as well.

namespace WriteToFile
{
    class Program
    {
        static void Main(string[] args)
        {

            // Gain access to the current directory
            DirectoryInfo currentDirectory = new DirectoryInfo(".");

            // To access a different directory. Use @ before the string to enable the use of backslashes
            // Set the variable to the directory of your choice, I'm using my user directory
            DirectoryInfo fileDirectory = new DirectoryInfo(@"C:\Users\Clifton");

            // Displays the directory path, name, parent directory, the attribute and time created
            Console.WriteLine(fileDirectory.FullName);
            Console.WriteLine(fileDirectory.Name);
            Console.WriteLine(fileDirectory.Parent);
            Console.WriteLine(fileDirectory.Attributes);
            Console.WriteLine(fileDirectory.CreationTime);

            // Creates a new directory. Texts is a folder created by this statement
            Directory.CreateDirectory(@"C:\Users\Clifton\Texts");

            // to delete a directory:
            // Directory.Delete(@"Directory\Path"); 


            //_______ TO READ AND WRITE FILES _______\\

            // Creating and array of names for the file


            string[] names = new string[3];
            
                names[0] = "Neil Diamond";
                names[1] = "Ozzy Osbourne";
                names[2] = "Meat Loaf";
            

            // This was the code the tutorial used, though it did not work for me
            // with the foreach loop below. The output read System.String[] instead of Singers

            //string[] names =
            //{
            //    "Neil Diamond",
            //    "Ozzy Osbourne",
            //    "Meat Loaf"
            //};

            // Defining the path to a file to be created
            string textFilePath = @"C:\Users\Clifton\Texts\testFile2.txt";

            // Writes the array to the file
            File.WriteAllLines(textFilePath, names);

            for (int arrayIndex = 0; arrayIndex < names.Length; arrayIndex++)
            {
                Console.WriteLine("Singers : " + names[arrayIndex]);
            }

            // Like above, this foreach loop wouldn't work. So I went with the for loop instead
            // though I would have liked to use the ReadAllLines method

            //foreach(string names in File.ReadAllLines(textFilePath))
            //{
            //    Console.WriteLine($"Singer : {names}");
            //}


            // Assigns a variable for the file path that I created
            DirectoryInfo myTexts = new DirectoryInfo(@"C:\Users\Clifton\Texts");

            // This will get the all the text files within the directory and subdirectories
            FileInfo[] txtFiles = myTexts.GetFiles("*.txt", SearchOption.AllDirectories);

            // Display the number of items matching the criteria of *.txt
            Console.WriteLine("Matches : {0}", txtFiles.Length);

            // Displays the names and number of bytes of the files found and stored in the txtFiles array
            foreach(FileInfo file in txtFiles)
            {
                Console.WriteLine(file.Name);
                Console.WriteLine(file.Length);
            }

            //______File Stream______\\
            // FileStream is for reading or writing a byte, or array of bytes

            // Assigns a new file path
            string textFilePath2 = @"C:\Users\Clifton\Texts\txtFile3.txt";

            // Opens the file, or creates the file if not yet created
            FileStream fs = File.Open(textFilePath2, FileMode.Create);

            // Converts a string into a byte array
            string randString = "This is a random string";
            byte[] rsByteArray = Encoding.Default.GetBytes(randString);

            // To write to the file, define the byte array and the index to start writing from
            fs.Write(rsByteArray, 0, rsByteArray.Length);

            // Changes the FileStream's position back to the beginning of the FileStream
            fs.Position = 0;

            // Byte array to hold the file data
            // Also puts in the length of the array since it was obtained prior, and pass it into 
            // the array by calling .Length
            byte[] fileByteArray = new byte[rsByteArray.Length];

            // Starts the index at the 0 position in the array and cycles through as long as i
            // is smaller than the length of the array, incrementing i + 1 each time
            for (int i = 0; i < rsByteArray.Length; i++)
            {
                // Converts the index of fileByteArray into bytes and reads those bytes
                fileByteArray[i] = (byte)fs.ReadByte();
            }

            //Converts the bytes in the array back into string and prints it
            Console.WriteLine(Encoding.Default.GetString(fileByteArray));

            // Closes the FileStream
            fs.Close();

            //_____StreamWriter and StreamReader______\\
            // Best for reading and writing strings

            // Declares the file path
            string textFilePath3 = @"C:\Users\Clifton\Texts\txtFile4.txt";

            // Creates the text tile
            StreamWriter sw = File.CreateText(textFilePath3);

            // Writes to the file without a new line, and with a new line.
            sw.Write("This is a random ");
            sw.WriteLine("sentence");
            sw.WriteLine("This is another sentence");

            // Closes the StreamWriter
            sw.Close();

            // Opens the file for reading
            StreamReader sr = File.OpenText(textFilePath3);

            // Peek will return the next character as a unicode number
            // Converting from the unicode number into a character
            // It returns the value but still stays at the beginning of the file 
            Console.WriteLine("Peek : {0}",
                Convert.ToChar(sr.Peek()));

            // Reads the first string up to a new line
            Console.WriteLine("1st String : {0}",
                sr.ReadLine());

            // Reads to the end of the file, everything left that hasn't already been read
            Console.WriteLine("Everything : {0}",
                sr.ReadToEnd());

            // Closes the StreamReader
            sr.Close();

            //_____BinaryWriter and Binary Reader______\\
            // Reads and writes datatypes

            // Iniciates the filepath
            string textFilePath4 = @"C:\Users\Clifton\Texts\File5.dat";

            // Gets the file
            FileInfo datFile = new FileInfo(textFilePath4);

            // Opens the file for writing
            BinaryWriter bw = new BinaryWriter(datFile.OpenWrite());

            // Information to store into the file
            string randText = "Random Text";
            int myAge = 31;
            double height = 6.33;

            // Writes the information
            bw.Write(randText);
            bw.Write(myAge);
            bw.Write(height);

            // Closes BinaryWriter
            bw.Close();

            // Opens the file for reading
            BinaryReader br = new BinaryReader(datFile.OpenRead());

            // Outputs the information stored in the dat file, read by their datatypes
            Console.WriteLine(br.ReadString());
            Console.WriteLine(br.ReadInt32());
            Console.WriteLine(br.ReadDouble());

            // Closes the Reader
            br.Close();

            Console.ReadLine();

        }
    }
}
