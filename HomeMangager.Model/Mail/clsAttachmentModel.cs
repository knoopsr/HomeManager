namespace HomeManager.Model.Mail
{
    /// <summary>
    /// Bevat informatie over een bestand dat als bijlage bij een e-mail wordt verzonden.
    /// </summary>
    public class clsAttachmentModel
    {
        #region Properties

        /// <summary>
        /// De bestandsnaam van de bijlage (inclusief extensie).
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// De bestandsinhoud in byte-array formaat.
        /// </summary>
        public byte[] FileData { get; set; }

        /// <summary>
        /// De MIME-type van het bestand, zoals "application/pdf" of "image/png".
        /// </summary>
        public string ContentType { get; set; }

        #endregion
    }
}
