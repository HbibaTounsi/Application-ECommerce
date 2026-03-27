using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;



namespace Application_ECommerce.Core.Interfaces
{
    public interface IFileHelper
    {
        // Télécharge un fichier vers le serveur et retourne son chemin (ou null si échec)
        public string? UploadFile(IFormFile file, string folder);

        // Supprime un fichier du serveur (retourne true si succès)
        public bool DeleteFile(string Path, string folder);
    }
}