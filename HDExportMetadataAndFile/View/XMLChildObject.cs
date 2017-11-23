using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace HDExportMetadataAndFile.View
{
    public class XMLChildObject
    {
        public string rootID { get; set; }
        public string rootScheduleDate { get; set; }        

        public string contentID { get; set; }        
        public string contentTitle{ get; set; }
        public string contentDuration { get; set; }
        public string contentAction { get; set; }
        public string contentStartDate { get; set; }
        public string contentViTitle { get; set; }
        public string contentViDescription { get; set; }
        public string contentViCopyright { get; set; }
        public string contentViSynopsis { get; set; }
        public string contentPromoImages { get; set; }
        public string contentAspect { get; set; }
        public string contentIsRecordable { get; set; }
        public string contentYear { get; set; }
        public string contentRating { get; set; }
        public string contentCategories { get; set; }
        public string contentActors { get; set; }
        public string contentDirectors { get; set; }
        public string contentLanguage { get; set; }
        public string contentSubtitles { get; set; }
        public string contentStudio { get; set; }
        public string contentCountries { get; set; }
        public string contentServiceId { get; set; }
        public string contentMediaID { get; set; }
        public string contentMediaFilename { get; set; }
        public string contentMediaFileSize { get; set; }
        public string contentMediaFormat { get; set; }
        public string contentMediaFrameDuration { get; set; }
        public string contentMediaSrcAssetType { get; set; }
        public string contentMediaAction { get; set; }
        public string contentMediaStorageDevice { get; set; }
        public string contentMediaType { get; set; }
        public string contentMediaRelativePath { get; set; }
        public string contentContentID { get; set; }
        public string contentContentTitle { get; set; }
        public string contentContentPreLoaded { get; set; }
        public string contentContentStartDate { get; set; }
        public string contentContentEncProfileName { get; set; }
        public string contentContentAction { get; set; }
        public string contentContentDefinition { get; set; }

        public string imageID { get; set; }
        public string imageTitle { get; set; }
        public string imageIllustratedRef { get; set; }
        public string imageAction { get; set; }
        public string imageStartDate { get; set; }
        public string imageExpiryDate { get; set; }
        public string imageMediaID { get; set; }
        public string imageMediaFileName { get; set; }
        public string imageMediaSrcAssetType { get; set; }
        public string imageMediaFrameDuration { get; set; }
        public string imageMediaFileSize { get; set; }
        public string imageMediaComment { get; set; }
        public string imageMediaFormat { get; set; }
        public string imageMediaStorageDevice { get; set; }
        public string imageMediaType { get; set; }
        public string imageMediaRelativePath { get; set; }
        public string imageImageID { get; set; }
        public string imageImageTitle { get; set; }
        public string imageImageAction { get; set; }
        public string imageImageStartdate { get; set; }
        public string imageImageExpiryDate { get; set; }
        public string imageImageEncProfileName { get; set; }
        public string imageImagePreLoaded { get; set; }

        public string image2ID { get; set; }
        public string image2Title { get; set; }
        public string image2IllustratedRef { get; set; }
        public string image2Action { get; set; }
        public string image2StartDate { get; set; }
        public string image2ExpiryDate { get; set; }
        public string image2MediaID { get; set; }
        public string image2MediaFileName { get; set; }
        public string image2MediaSrcAssetType { get; set; }
        public string image2MediaFrameDuration { get; set; }
        public string image2MediaFileSize { get; set; }
        public string image2MediaComment { get; set; }
        public string image2MediaFormat { get; set; }
        public string image2MediaStorageDevice { get; set; }
        public string image2MediaType { get; set; }
        public string image2MediaRelativePath { get; set; }
        public string image2ImageID { get; set; }
        public string image2ImageTitle { get; set; }
        public string image2ImageAction { get; set; }
        public string image2ImageStartdate { get; set; }
        public string image2ImageExpiryDate { get; set; }
        public string image2ImageEncProfileName { get; set; }
        public string image2ImagePreLoaded { get; set; }

        public string vodItemID { get; set; }
        public string vodItemContentRef { get; set; }
        public string vodItemNodeRefList { get; set; }
        public string vodItemTitle { get; set; }
        public string vodItemAction { get; set; }
        public string vodItemDisplayPriority { get; set; }
        public string vodItemPeriodStart { get; set; }
        public string vodItemPeriodEnd { get; set; }

        public string productID { get; set; }
        public string productAction { get; set; }
        public string productCurrency { get; set; }
        public string productPrice { get; set; }
        public string productType { get; set; }
        public string productTitle { get; set; }
        public string productElementKind { get; set; }
        public string productElementId { get; set; }
        public XMLChildObject()
        {            
            rootID = "GLOBAL";                       
        }        
    }

    public class XMLLongChildObject
    {
        public string rootID { get; set; }
        public string rootScheduleDate { get; set; }

        public string seriesID { get; set; }
        public string seriesTitle { get; set; }
        public string seriesAction { get; set; }
        public string seriesViTitle { get; set; }
        public string seriesViSynopsis { get; set; }
        public string seriesEnTitle { get; set; }
        public string seriesPromoImages { get; set; }
        public string seriesCategories { get; set; }
        public string seriesRating { get; set; }

        public string contentID { get; set; }
        public string contentNumber { get; set; }
        public string contentSeriesRef { get; set; }
        public string contentTitle { get; set; }
        public string contentDuration { get; set; }
        public string contentAction { get; set; }
        public string contentStartDate { get; set; }
        public string contentViTitle { get; set; }
        public string contentViDescription { get; set; }
        public string contentViCopyright { get; set; }
        public string contentViSynopsis { get; set; }
        public string contentPromoImages { get; set; }
        public string contentAspect { get; set; }
        public string contentIsRecordable { get; set; }
        public string contentYear { get; set; }
        public string contentRating { get; set; }
        public string contentCategories { get; set; }
        public string contentActors { get; set; }
        public string contentDirectors { get; set; }
        public string contentLanguage { get; set; }
        public string contentSubtitles { get; set; }
        public string contentStudio { get; set; }
        public string contentCountries { get; set; }      
        public string contentServiceId { get; set; }
        public string contentMediaID { get; set; }
        public string contentMediaFilename { get; set; }
        public string contentMediaFileSize { get; set; }
        public string contentMediaFormat { get; set; }
        public string contentMediaFrameDuration { get; set; }
        public string contentMediaSrcAssetType { get; set; }
        public string contentMediaAction { get; set; }
        public string contentMediaStorageDevice { get; set; }
        public string contentMediaType { get; set; }
        public string contentMediaRelativePath { get; set; }
        public string contentContentID { get; set; }
        public string contentContentTitle { get; set; }
        public string contentContentPreLoaded { get; set; }
        public string contentContentStartDate { get; set; }
        public string contentContentEncProfileName { get; set; }
        public string contentContentAction { get; set; }
        public string contentContentDefinition { get; set; }

        public string imageID { get; set; }
        public string imageTitle { get; set; }
        public string imageIllustratedRef { get; set; }
        public string imageAction { get; set; }
        public string imageStartDate { get; set; }
        public string imageExpiryDate { get; set; }
        public string imageMediaID { get; set; }
        public string imageMediaFileName { get; set; }
        public string imageMediaSrcAssetType { get; set; }
        public string imageMediaFrameDuration { get; set; }
        public string imageMediaFileSize { get; set; }
        public string imageMediaComment { get; set; }
        public string imageMediaFormat { get; set; }
        public string imageMediaStorageDevice { get; set; }
        public string imageMediaType { get; set; }
        public string imageMediaRelativePath { get; set; }
        public string imageImageID { get; set; }
        public string imageImageTitle { get; set; }
        public string imageImageAction { get; set; }
        public string imageImageStartdate { get; set; }
        public string imageImageExpiryDate { get; set; }
        public string imageImageEncProfileName { get; set; }
        public string imageImagePreLoaded { get; set; }

        public string image2ID { get; set; }
        public string image2Title { get; set; }
        public string image2IllustratedRef { get; set; }
        public string image2Action { get; set; }
        public string image2StartDate { get; set; }
        public string image2ExpiryDate { get; set; }
        public string image2MediaID { get; set; }
        public string image2MediaFileName { get; set; }
        public string image2MediaSrcAssetType { get; set; }
        public string image2MediaFrameDuration { get; set; }
        public string image2MediaFileSize { get; set; }
        public string image2MediaComment { get; set; }
        public string image2MediaFormat { get; set; }
        public string image2MediaStorageDevice { get; set; }
        public string image2MediaType { get; set; }
        public string image2MediaRelativePath { get; set; }
        public string image2ImageID { get; set; }
        public string image2ImageTitle { get; set; }
        public string image2ImageAction { get; set; }
        public string image2ImageStartdate { get; set; }
        public string image2ImageExpiryDate { get; set; }
        public string image2ImageEncProfileName { get; set; }
        public string image2ImagePreLoaded { get; set; }

        public string vodItemID { get; set; }
        public string vodItemContentRef { get; set; }
        public string vodItemNodeRefList { get; set; }
        public string vodItemTitle { get; set; }
        public string vodItemAction { get; set; }
        public string vodItemDisplayPriority { get; set; }
        public string vodItemPeriodStart { get; set; }
        public string vodItemPeriodEnd { get; set; }

        public string productID { get; set; }
        public string productAction { get; set; }
        public string productCurrency { get; set; }
        public string productPrice { get; set; }
        public string productType { get; set; }
        public string productTitle { get; set; }
        public string productElementKind { get; set; }
        public string productElementId { get; set; }
        public XMLLongChildObject()
        {
            rootID = "GLOBAL";
        }
    }

    public class XMLShortChildObject
    {
        public string rootID { get; set; }
        public string rootScheduleDate { get; set; }

        public string contentID { get; set; }
        public string contentNumber { get; set; }
        public string contentSeriesRef { get; set; }
        public string contentTitle { get; set; }
        public string contentDuration { get; set; }
        public string contentAction { get; set; }
        public string contentStartDate { get; set; }
        public string contentViTitle { get; set; }
        public string contentViDescription { get; set; }
        public string contentViCopyright { get; set; }
        public string contentViSynopsis { get; set; }
        public string contentPromoImages { get; set; }
        public string contentAspect { get; set; }
        public string contentIsRecordable { get; set; }
        public string contentYear { get; set; }
        public string contentRating { get; set; }
        public string contentCategories { get; set; }
        public string contentActors { get; set; }
        public string contentDirectors { get; set; }
        public string contentLanguage { get; set; }
        public string contentSubtitles { get; set; }
        public string contentStudio { get; set; }
        public string contentCountries { get; set; }       
        public string contentMediaID { get; set; }
        public string contentMediaFilename { get; set; }
        public string contentMediaFileSize { get; set; }
        public string contentMediaFormat { get; set; }
        public string contentMediaFrameDuration { get; set; }
        public string contentMediaSrcAssetType { get; set; }
        public string contentMediaAction { get; set; }
        public string contentMediaStorageDevice { get; set; }
        public string contentMediaType { get; set; }
        public string contentMediaRelativePath { get; set; }
        public string contentContentID { get; set; }
        public string contentContentTitle { get; set; }
        public string contentContentPreLoaded { get; set; }
        public string contentContentStartDate { get; set; }
        public string contentContentEncProfileName { get; set; }
        public string contentContentAction { get; set; }
        public string contentContentDefinition { get; set; }
                
        public string vodItemID { get; set; }
        public string vodItemContentRef { get; set; }
        public string vodItemNodeRefList { get; set; }
        public string vodItemTitle { get; set; }
        public string vodItemAction { get; set; }
        public string vodItemDisplayPriority { get; set; }
        public string vodItemPeriodStart { get; set; }
        public string vodItemPeriodEnd { get; set; }

        public string productID { get; set; }
        public string productAction { get; set; }
        public string productCurrency { get; set; }
        public string productPrice { get; set; }
        public string productType { get; set; }
        public string productTitle { get; set; }
        public string productElementKind { get; set; }
        public string productElementId { get; set; }
        public XMLShortChildObject()
        {
            rootID = "GLOBAL";
        }
    }
}
