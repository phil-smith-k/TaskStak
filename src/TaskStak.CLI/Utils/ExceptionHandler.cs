using System.Text.Json;
using TaskStak.CLI.Models;

namespace TaskStak.CLI.Utils
{
    public static class ExceptionHandler
    {
        private static readonly string FilePath = JsonHelper.GetTasksFilePath();

        public static int HandleGlobalException(Exception exception)
        {
            switch (exception)
            {
                case TaskStakException taskStak: 
                    HandleTaskStakException(taskStak);
                    break;
                case JsonException jsonEx: 
                    HandleJsonException(jsonEx);
                    break;
                case UnauthorizedAccessException accessEx: 
                    HandleUnauthorizedAccessException(accessEx);
                    break;
                case DirectoryNotFoundException dirEx: 
                    HandleDirectoryNotFoundException(dirEx);
                    break;
                case IOException ioEx when IsDiskSpaceError(ioEx): 
                    HandleDiskSpaceException(ioEx);
                    break;
                case IOException ioEx: 
                    HandleGenericIOException(ioEx);
                    break;
                default: 
                    HandleUnexpectedException(exception);
                    break;
            }

            return 1;
        }

        private static void HandleTaskStakException(TaskStakException ex)
        {
            Console.Error.WriteLine($"TaskStak: {ex.Message}");
        }

        //TODO: Messages too long. Break out into separate lines. 
        private static void HandleJsonException(JsonException ex)
        {
            Console.Error.WriteLine($"TaskStak: Task data file is corrupted and cannot be read. File location: {FilePath} - You may need to restore from backup, delete, or modify the file to resolve error.");
#if DEBUG
            Console.Error.WriteLine($"JSON error details: {ex.Message}");
#endif
        }

        private static void HandleUnauthorizedAccessException(UnauthorizedAccessException ex)
        {
            Console.Error.WriteLine($"TaskStak: Permission denied accessing task storage. Unable to access: {FilePath} - Try running with elevated permissions or check directory permissions.");
        }

        private static void HandleDirectoryNotFoundException(DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"TaskStak: Cannot access task storage directory. Missing directory: {FilePath} - Check that your home directory is accessible.");
        }

        private static void HandleDiskSpaceException(IOException ex)
        {
            Console.Error.WriteLine($"TaskStak: Cannot save tasks due to disk space or I/O error. Target location: {FilePath} - Check available disk space and try again.");
        }

        private static void HandleGenericIOException(IOException ex)
        {
            Console.Error.WriteLine($"TaskStak: File system error occurred. Location: {FilePath} - Error: {ex.Message}");
        }

        private static void HandleUnexpectedException(Exception ex)
        {
            Console.Error.WriteLine("TaskStak: An unexpected error occurred.");
#if DEBUG
            Console.Error.WriteLine($"Full details: {ex}");
#endif
        }

        private static bool IsDiskSpaceError(IOException ex)
        {
            var message = ex.Message.ToLowerInvariant();
            return message.Contains("disk") ||
                   message.Contains("space") ||
                   message.Contains("full") ||
                   ex.HResult == -2147024784; // ERROR_DISK_FULL
        }
    }
}
