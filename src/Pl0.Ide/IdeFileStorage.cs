namespace Pl0.Ide;

internal interface IIdeFileStorage
{
    string ReadAllText(string path);
    void WriteAllText(string path, string content);
}

internal sealed class PhysicalIdeFileStorage : IIdeFileStorage
{
    public string ReadAllText(string path)
    {
        return File.ReadAllText(path);
    }

    public void WriteAllText(string path, string content)
    {
        File.WriteAllText(path, content);
    }
}
