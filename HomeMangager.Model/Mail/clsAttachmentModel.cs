namespace HomeManager.Model.Mail
{
    public class clsAttachmentModel
    {

        public string FileName { get; set; }      // Naam van het bestand
        public byte[] FileData { get; set; }      // Bestandsinhoud in bytes
        public string ContentType { get; set; }   // MIME-type (bv. "application/pdf" of "image/png")

    }
}
