/*
Generates image filenames for Obsidian MarkDown notes by appending an incr. number

E.g. image.jpg -> image-1.jpg
*/

using System;
using System.IO;
using System.Windows.Forms;

class Program
{
    [STAThread]
    static void Main()
    {
        string input = "image.jpg"; 
        string filename = Path.GetFileNameWithoutExtension(input);
        string ext = Path.GetExtension(filename);
        int limit = 23;
        int counter = 1;

        using (StringWriter stringWriter = new StringWriter())
        {
            Console.WriteLine(input); // Prints as redundancy
            stringWriter.WriteLine(input); // Add orig. filename to clipboard list

            // Iter. up to the limit
            while (counter <= limit)
            {
                string newFilename = $"{filename}-{counter}{ext}"; 
                Console.WriteLine(newFilename); // Redundancy in case clipboard fails
                stringWriter.WriteLine(newFilename); // Adds filename iter. to clipboard
                counter++; // Incr. counter to reach user req. limit
            }

            Clipboard.SetText(stringWriter.ToString()); // The whole enchilada
        }

        Console.WriteLine("Print output copied to clipboard.");
    }
}