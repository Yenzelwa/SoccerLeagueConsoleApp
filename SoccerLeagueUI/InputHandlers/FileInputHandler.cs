using Microsoft.Extensions.Logging;

namespace SoccerLeagueUI.InputHandlers
{
    public class FileInputHandler : IFileInputHandler
    {
        private readonly ILogger<FileInputHandler> _logger;

        public FileInputHandler(ILogger<FileInputHandler> logger)
        {
            _logger = logger;
        }

        public List<string> ReadData()
        {
            _logger.LogInformation("File input handling started.");

            string filePath = GetFilePathFromUser();

            if (string.IsNullOrEmpty(filePath))
            {
                _logger.LogWarning("File path is empty or not provided.");
                return new List<string>();
            }

            if (!FileExists(filePath))
            {
                _logger.LogWarning($"File not found at path: {filePath}");
                return new List<string>();
            }

            return ReadFileContents(filePath);
        }

        private string GetFilePathFromUser()
        {
            Console.Write("Enter the path to the file containing match data: ");
            return Console.ReadLine();
        }

        private bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        private List<string> ReadFileContents(string filePath)
        {
            try
            {
                _logger.LogInformation($"Reading file contents from path: {filePath}");
                string[] lines = File.ReadAllLines(filePath);
                return new List<string>(lines);
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "An error occurred while reading the file: {ErrorMessage}", ex.Message);
                return new List<string>();
            }
        }
    }
}
