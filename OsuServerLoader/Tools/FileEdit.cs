using System.IO;

namespace OsuServerLoader.Tools
{
    internal class FileEdit
    {
        public void ChangeAccountForOsu(string path, string name, string password, string CEServer)
        {
            string[] lines = File.ReadAllLines(path);

            for (int i = 0; i < lines.Length; i++)
            {
                if (i == 140)
                {
                    lines[i] = "CredentialEndpoint = " + CEServer;
                }
                if (i == 141)
                {
                    lines[i] = "Username = " + name;
                }
                if (i == 148)
                {
                    lines[i] = "Password = " + password;
                }
            }

            File.WriteAllLines(path, lines);
        }
    }
}
