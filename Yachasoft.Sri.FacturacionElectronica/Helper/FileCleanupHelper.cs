using System;
using System.IO;
using System.Threading.Tasks;

namespace Yachasoft.Sri.FacturacionElectronica.Services
{
    public static class FileCleanupHelper
    {
        /// <summary>
        /// Elimina un archivo local de forma segura si existe.
        /// </summary>
        /// <param name="filePath">Ruta completa del archivo</param>
        public static async Task<bool> DeleteFileAsync(string filePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    Console.WriteLine("[⚠️] Ruta de archivo vacía o nula, no se eliminará.");
                    return false;
                }

                if (File.Exists(filePath))
                {
                    await Task.Run(() => File.Delete(filePath));
                    Console.WriteLine($"[🗑️] Archivo eliminado correctamente: {filePath}");
                    return true;
                }
                else
                {
                    Console.WriteLine($"[ℹ️] No se encontró el archivo para eliminar: {filePath}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[❌] Error al eliminar archivo {filePath}: {ex.Message}");
                return false;
            }
        }
    }
}
