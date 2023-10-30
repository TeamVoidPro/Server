using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Server.Models.Dto
{
    public class OnSiteVerificationsDto
    {
        [Key]
        [Column(TypeName = "varchar(20)")]
        public required string OwnerId { get; set; }

        public required bool EasyAccess { get; set; }

        public required bool SlotCount { get; set; }

        public required bool SlotDimensions { get; set; }

        public required bool LandDimensions { get; set; }

        public required bool DocumentPresentation { get; set; }

        [AllowNull]
        [Column(TypeName = "varchar(512)")]
        public string NoDocumentReason { get; set; }

        public required bool SerialMatch { get; set; }

        [AllowNull]
        [Column(TypeName = "varchar(512)")]
        public string NoSerialMatchReason { get; set; }

        [AllowNull]
        [Column(TypeName = "varchar(512)")]
        public string Concerns { get; set; }

        public required bool SuitablePlace { get; set; }

        [AllowNull]
        [Column(TypeName = "varchar(512)")]
        public string NotSuitableReasons { get; set; }

        public required bool DisasterProne { get; set; }

        [AllowNull]
        [Column(TypeName = "varchar(512)")]
        public string DisasterReasons { get; set; }

        [Column(TypeName = "varchar(512)")]
        [Required(ErrorMessage = "A description is required")]
        public required string GeneralImpression { get; set; }

        [Required(ErrorMessage = "Images are required")]
        [Column(TypeName = "varchar(256)")]
        public required string Images { get; set; }

    }
}
