using System;
using System.ComponentModel.DataAnnotations;
using Common;

namespace NbSites.Web.Libs.Domain
{
    public class LinkItem
    {
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "名称")]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "链接")]
        public string Href { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "描述")]
        public string Description { get; set; }
        [Display(Name = "排序码")]
        public float Sort { get; set; }
        [Display(Name = "离线")]
        public bool OffLine { get; set; }

        public MessageResult ValidateSelf()
        {
            var validateResult = MessageResult.ValidateResult();
            if (string.IsNullOrWhiteSpace(Title))
            {
                validateResult.Message = "名称不能为空";
                return validateResult;
            }
            if (string.IsNullOrWhiteSpace(Href))
            {
                validateResult.Message = "链接不能为空";
                return validateResult;
            }

            validateResult.Success = true;
            return validateResult;
        }
    }
}
