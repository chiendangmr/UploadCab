using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace HDExportMetadataAndFile.View
{
    public class XMLObject
    {
        XmlDocument xmlDoc = null;       
        XmlNode rootNode = null;
                
        XmlNode elementContent = null;
        XmlNode elementImage = null;
        XmlNode elementImage2 = null;
        XmlNode elementVodItem = null;
        XmlNode elementProduct = null;

        XmlAttribute attRootSchedule = null;
        XmlAttribute attRootID = null;        

        XmlAttribute attContentID = null;        
        XmlAttribute attContentTitle = null;
        XmlAttribute attContentDuration = null;
        XmlAttribute attContentAction = null;
        XmlAttribute attContentStartDate = null;

        XmlAttribute attImageID = null;
        XmlAttribute attImageTitle = null;
        XmlAttribute attImageIllutratedRef = null;
        XmlAttribute attImageAction = null;
        XmlAttribute attImageStartDate = null;
        XmlAttribute attImageExpiryDate = null;

        XmlAttribute attImage2ID = null;
        XmlAttribute attImage2Title = null;
        XmlAttribute attImage2IllutratedRef = null;
        XmlAttribute attImage2Action = null;
        XmlAttribute attImage2StartDate = null;
        XmlAttribute attImage2ExpiryDate = null;

        XmlAttribute attVodItemID = null;
        XmlAttribute attVodItemContentRef = null;
        XmlAttribute attVodItemNodeRefList = null;
        XmlAttribute attVodItemTitle = null;
        XmlAttribute attVodItemAction = null;

        XmlAttribute attProductID = null;
        XmlAttribute attProductAction = null;
        XmlAttribute attProductCurrency = null;
        XmlAttribute attProductPrice = null;
        XmlAttribute attProductType = null;
        XmlAttribute attProductTitle = null;        
       
        public XMLObject()
        {
            xmlDoc = new XmlDocument();
            rootNode = xmlDoc.CreateElement("LysisData");
            xmlDoc.AppendChild(rootNode);  
            
        }
        public void GenerateXml(XMLChildObject tempObj)
        {
            try
            {
                attRootSchedule = creatXmlAtt(xmlDoc, rootNode, "scheduleDate", tempObj.rootScheduleDate);
                attRootID = creatXmlAtt(xmlDoc, rootNode, "id", tempObj.rootID);             

                #region element Content
                elementContent = addNodeChild(xmlDoc, "Content", rootNode);
                attContentID = creatXmlAtt(xmlDoc, elementContent, "id", tempObj.contentID);               
                attContentTitle = creatXmlAtt(xmlDoc, elementContent, "title", tempObj.contentTitle);
                attContentDuration = creatXmlAtt(xmlDoc, elementContent, "duration", tempObj.contentDuration);
                attContentAction = creatXmlAtt(xmlDoc, elementContent, "action", tempObj.contentAction);
                attContentStartDate = creatXmlAtt(xmlDoc, elementContent, "startDate", tempObj.contentStartDate);

                XmlNode contentChild1 = addNodeChild(this.xmlDoc, "EpgDescription", elementContent);
                XmlAttribute contentChild1Att = creatXmlAtt(this.xmlDoc, contentChild1, "locale", "vi_VN");
                XmlNode contentChild1OfChild1 = addNodeChild(this.xmlDoc, "EpgElement", contentChild1, tempObj.contentViTitle);
                XmlAttribute contentChild1OfChild1Att = creatXmlAtt(this.xmlDoc, contentChild1OfChild1, "key", "Title");
                XmlNode contentChild1OfChild2 = addNodeChild(this.xmlDoc, "EpgElement", contentChild1, tempObj.contentViDescription);
                XmlAttribute contentChild1OfChild2Att = creatXmlAtt(this.xmlDoc, contentChild1OfChild2, "key", "Description");
                XmlNode contentChild1OfChild3 = addNodeChild(this.xmlDoc, "EpgElement", contentChild1, tempObj.contentViCopyright);
                XmlAttribute contentChild1OfChild3Att = creatXmlAtt(this.xmlDoc, contentChild1OfChild3, "key", "Copyright");
                XmlNode contentChild1OfChild4 = addNodeChild(this.xmlDoc, "EpgElement", contentChild1, tempObj.contentViSynopsis);
                XmlAttribute contentChild1OfChild4Att = creatXmlAtt(this.xmlDoc, contentChild1OfChild4, "key", "Synopsis");

                XmlNode contentChild2 = addNodeChild(this.xmlDoc, "EpgDescription", elementContent);
                XmlNode contentChild2OfChild1 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentPromoImages);
                XmlAttribute contentChild2OfChild1Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild1, "key", "PromoImages");
                XmlNode contentChild2OfChild2 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentAspect);
                XmlAttribute contentChild2OfChild2Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild2, "key", "Aspect");
                XmlNode contentChild2OfChild3 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentIsRecordable);
                XmlAttribute contentChild2OfChild3Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild3, "key", "IsRecordable");
                XmlNode contentChild2OfChild4 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentYear);
                XmlAttribute contentChild2OfChild4Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild4, "key", "Year");
                XmlNode contentChild2OfChild5 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentRating);
                XmlAttribute contentChild2OfChild5Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild5, "key", "Rating");
                XmlNode contentChild2OfChild6 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentCategories);
                XmlAttribute contentChild2OfChild6Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild6, "key", "Categories");
                XmlNode contentChild2OfChild7 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentActors);
                XmlAttribute contentChild2OfChild7Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild7, "key", "Actors");
                XmlNode contentChild2OfChild8 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentDirectors);
                XmlAttribute contentChild2OfChild8Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild8, "key", "Directors");
                XmlNode contentChild2OfChild9 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentLanguage);
                XmlAttribute contentChild2OfChild9Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild9, "key", "Language");
                XmlNode contentChild2OfChild10 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentSubtitles);
                XmlAttribute contentChild2OfChild10Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild10, "key", "Subtitles");
                XmlNode contentChild2OfChild11 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentStudio);
                XmlAttribute contentChild2OfChild11Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild11, "key", "Studio");
                XmlNode contentChild2OfChild12 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentCountries);
                XmlAttribute contentChild2OfChild12Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild12, "key", "Countries");
                XmlNode contentChild2OfChild13 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentServiceId);
                XmlAttribute contentChild2OfChild13Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild13, "key", "ServiceId");

                XmlNode contentChild3 = addNodeChild(this.xmlDoc, "Media", elementContent);
                XmlAttribute contentChild3Att1 = creatXmlAtt(this.xmlDoc, contentChild3, "id", tempObj.contentMediaID);
                XmlAttribute contentChild3Att2 = creatXmlAtt(this.xmlDoc, contentChild3, "fileName", tempObj.contentMediaFilename);
                XmlAttribute contentChild3Att3 = creatXmlAtt(this.xmlDoc, contentChild3, "fileSize", tempObj.contentMediaFileSize);
                XmlAttribute contentChild3Att4 = creatXmlAtt(this.xmlDoc, contentChild3, "format", tempObj.contentMediaFormat);
                XmlAttribute contentChild3Att5 = creatXmlAtt(this.xmlDoc, contentChild3, "frameDuration", tempObj.contentMediaFrameDuration);
                XmlAttribute contentChild3Att6 = creatXmlAtt(this.xmlDoc, contentChild3, "srcAssetType", tempObj.contentMediaSrcAssetType);
                XmlAttribute contentChild3Att7 = creatXmlAtt(this.xmlDoc, contentChild3, "action", tempObj.contentMediaAction);

                XmlNode contentChild3OfChild = addNodeChild(this.xmlDoc, "AssetDeviceLink", contentChild3);
                XmlAttribute contentChild3OfChild1Att = creatXmlAtt(this.xmlDoc, contentChild3OfChild, "storageDevice", tempObj.contentMediaStorageDevice);
                XmlAttribute contentChild3OfChild2Att = creatXmlAtt(this.xmlDoc, contentChild3OfChild, "type", tempObj.contentMediaType);
                XmlAttribute contentChild3OfChild3Att = creatXmlAtt(this.xmlDoc, contentChild3OfChild, "relativePath", tempObj.contentMediaRelativePath);

                XmlNode contentChild4 = addNodeChild(this.xmlDoc, "Content", elementContent);
                XmlAttribute contentChild4Att1 = creatXmlAtt(this.xmlDoc, contentChild4, "id", tempObj.contentContentID);
                XmlAttribute contentChild4Att2 = creatXmlAtt(this.xmlDoc, contentChild4, "title", tempObj.contentContentTitle);
                XmlAttribute contentChild4Att3 = creatXmlAtt(this.xmlDoc, contentChild4, "preLoaded", tempObj.contentContentPreLoaded);
                XmlAttribute contentChild4Att4 = creatXmlAtt(this.xmlDoc, contentChild4, "startDate", tempObj.contentContentStartDate);
                XmlAttribute contentChild4Att5 = creatXmlAtt(this.xmlDoc, contentChild4, "encProfileName", tempObj.contentContentEncProfileName);
                XmlAttribute contentChild4Att6 = creatXmlAtt(this.xmlDoc, contentChild4, "action", tempObj.contentContentAction);

                XmlNode contentChild4OfChild = addNodeChild(this.xmlDoc, "EpgDescription", contentChild4);
                XmlNode contentChild4OfChildOfChild = addNodeChild(this.xmlDoc, "EpgElement", contentChild4OfChild, tempObj.contentContentDefinition);
                XmlAttribute contentChild4OfChildOfChildAtt = creatXmlAtt(this.xmlDoc, contentChild4OfChildOfChild, "key", "Definition");

                #endregion

                #region element Image
                elementImage = addNodeChild(xmlDoc, "Image", rootNode);
                attImageID = creatXmlAtt(xmlDoc, elementImage, "id", tempObj.imageID);
                attImageTitle = creatXmlAtt(xmlDoc, elementImage, "title", tempObj.imageTitle);
                attImageIllutratedRef = creatXmlAtt(xmlDoc, elementImage, "illustratedRef", tempObj.imageIllustratedRef);
                attImageAction = creatXmlAtt(xmlDoc, elementImage, "action", tempObj.imageAction);
                attImageStartDate = creatXmlAtt(xmlDoc, elementImage, "startDate", tempObj.imageStartDate);
                attImageExpiryDate = creatXmlAtt(xmlDoc, elementImage, "expiryDate", tempObj.imageExpiryDate);

                XmlNode imageChild1 = addNodeChild(this.xmlDoc, "Media", elementImage);
                XmlAttribute imageChild1Att1 = creatXmlAtt(this.xmlDoc, imageChild1, "id", tempObj.imageMediaID);
                XmlAttribute imageChild1Att2 = creatXmlAtt(this.xmlDoc, imageChild1, "fileName", tempObj.imageMediaFileName);
                XmlAttribute imageChild1Att3 = creatXmlAtt(this.xmlDoc, imageChild1, "srcAssetType", tempObj.imageMediaSrcAssetType);
                XmlAttribute imageChild1Att4 = creatXmlAtt(this.xmlDoc, imageChild1, "frameDuration", tempObj.imageMediaFrameDuration);
                XmlAttribute imageChild1Att5 = creatXmlAtt(this.xmlDoc, imageChild1, "fileSize", tempObj.imageMediaFileSize);
                XmlAttribute imageChild1Att6 = creatXmlAtt(this.xmlDoc, imageChild1, "comment", tempObj.imageMediaComment);
                XmlAttribute imageChild1Att7 = creatXmlAtt(this.xmlDoc, imageChild1, "format", tempObj.imageMediaFormat);

                XmlNode imageChild1OfChild = addNodeChild(this.xmlDoc, "AssetDeviceLink", imageChild1);
                XmlAttribute imageChild1OfChildAtt = creatXmlAtt(this.xmlDoc, imageChild1OfChild, "storageDevice", tempObj.imageMediaStorageDevice);
                XmlAttribute imageChild1OfChildAtt2 = creatXmlAtt(this.xmlDoc, imageChild1OfChild, "type", tempObj.imageMediaType);
                XmlAttribute imageChild1OfChildAtt3 = creatXmlAtt(this.xmlDoc, imageChild1OfChild, "relativePath", tempObj.imageMediaRelativePath);

                XmlNode imageChild2 = addNodeChild(this.xmlDoc, "Image", elementImage);
                XmlAttribute imageChild2Att1 = creatXmlAtt(this.xmlDoc, imageChild2, "id", tempObj.imageImageID);
                XmlAttribute imageChild2Att2 = creatXmlAtt(this.xmlDoc, imageChild2, "title", tempObj.imageImageTitle);
                XmlAttribute imageChild2Att3 = creatXmlAtt(this.xmlDoc, imageChild2, "action", tempObj.imageImageAction);
                XmlAttribute imageChild2Att4 = creatXmlAtt(this.xmlDoc, imageChild2, "startDate", tempObj.imageImageStartdate);
                XmlAttribute imageChild2Att5 = creatXmlAtt(this.xmlDoc, imageChild2, "expiryDate", tempObj.imageImageExpiryDate);
                XmlAttribute imageChild2Att6 = creatXmlAtt(this.xmlDoc, imageChild2, "encProfileName", tempObj.imageImageEncProfileName);
                XmlAttribute imageChild2Att7 = creatXmlAtt(this.xmlDoc, imageChild2, "preLoaded", tempObj.imageImagePreLoaded);
                #endregion

                #region element Image2
                elementImage2 = addNodeChild(xmlDoc, "Image", rootNode);
                attImage2ID = creatXmlAtt(xmlDoc, elementImage2, "id", tempObj.image2ID);
                attImage2Title = creatXmlAtt(xmlDoc, elementImage2, "title", tempObj.image2Title);
                attImage2IllutratedRef = creatXmlAtt(xmlDoc, elementImage2, "illustratedRef", tempObj.image2IllustratedRef);
                attImage2Action = creatXmlAtt(xmlDoc, elementImage2, "action", tempObj.image2Action);
                attImage2StartDate = creatXmlAtt(xmlDoc, elementImage2, "startDate", tempObj.image2StartDate);
                attImage2ExpiryDate = creatXmlAtt(xmlDoc, elementImage2, "expiryDate", tempObj.image2ExpiryDate);

                XmlNode image2Child1 = addNodeChild(this.xmlDoc, "Media", elementImage2);
                XmlAttribute image2Child1Att1 = creatXmlAtt(this.xmlDoc, image2Child1, "id", tempObj.image2MediaID);
                XmlAttribute image2Child1Att2 = creatXmlAtt(this.xmlDoc, image2Child1, "fileName", tempObj.image2MediaFileName);
                XmlAttribute image2Child1Att3 = creatXmlAtt(this.xmlDoc, image2Child1, "srcAssetType", tempObj.image2MediaSrcAssetType);
                XmlAttribute image2Child1Att4 = creatXmlAtt(this.xmlDoc, image2Child1, "frameDuration", tempObj.image2MediaFrameDuration);
                XmlAttribute image2Child1Att5 = creatXmlAtt(this.xmlDoc, image2Child1, "fileSize", tempObj.image2MediaFileSize);
                XmlAttribute image2Child1Att6 = creatXmlAtt(this.xmlDoc, image2Child1, "comment", tempObj.image2MediaComment);
                XmlAttribute image2Child1Att7 = creatXmlAtt(this.xmlDoc, image2Child1, "format", tempObj.image2MediaFormat);

                XmlNode image2Child1OfChild = addNodeChild(this.xmlDoc, "AssetDeviceLink", image2Child1);
                XmlAttribute image2Child1OfChildAtt = creatXmlAtt(this.xmlDoc, image2Child1OfChild, "storageDevice", tempObj.image2MediaStorageDevice);
                XmlAttribute image2Child1OfChildAtt2 = creatXmlAtt(this.xmlDoc, image2Child1OfChild, "type", tempObj.image2MediaType);
                XmlAttribute image2Child1OfChildAtt3 = creatXmlAtt(this.xmlDoc, image2Child1OfChild, "relativePath", tempObj.image2MediaRelativePath);

                XmlNode image2Child2 = addNodeChild(this.xmlDoc, "Image", elementImage2);
                XmlAttribute image2Child2Att1 = creatXmlAtt(this.xmlDoc, image2Child2, "id", tempObj.image2ImageID);
                XmlAttribute image2Child2Att2 = creatXmlAtt(this.xmlDoc, image2Child2, "title", tempObj.image2ImageTitle);
                XmlAttribute image2Child2Att3 = creatXmlAtt(this.xmlDoc, image2Child2, "action", tempObj.image2ImageAction);
                XmlAttribute image2Child2Att4 = creatXmlAtt(this.xmlDoc, image2Child2, "startDate", tempObj.image2ImageStartdate);
                XmlAttribute image2Child2Att5 = creatXmlAtt(this.xmlDoc, image2Child2, "expiryDate", tempObj.image2ImageExpiryDate);
                XmlAttribute image2Child2Att6 = creatXmlAtt(this.xmlDoc, image2Child2, "encProfileName", tempObj.image2ImageEncProfileName);
                XmlAttribute image2Child2Att7 = creatXmlAtt(this.xmlDoc, image2Child2, "preLoaded", tempObj.image2ImagePreLoaded);
                #endregion

                #region element VodItem
                elementVodItem = addNodeChild(xmlDoc, "VodItem", rootNode);
                attVodItemID = creatXmlAtt(xmlDoc, elementVodItem, "id", tempObj.vodItemID);
                attVodItemContentRef = creatXmlAtt(xmlDoc, elementVodItem, "contentRef", tempObj.vodItemContentRef);
                attVodItemNodeRefList = creatXmlAtt(xmlDoc, elementVodItem, "nodeRefList", tempObj.vodItemNodeRefList);
                attVodItemTitle = creatXmlAtt(xmlDoc, elementVodItem, "title", tempObj.vodItemTitle);
                attVodItemAction = creatXmlAtt(xmlDoc, elementVodItem, "action", tempObj.vodItemAction);

                XmlNode vodItemChild1 = addNodeChild(this.xmlDoc, "EpgDescription", elementVodItem);
                XmlNode vodItemChild1OfChild1 = addNodeChild(this.xmlDoc, "EpgElement", vodItemChild1, tempObj.vodItemDisplayPriority);
                XmlAttribute vodItemChild1OfChild1Att1 = creatXmlAtt(this.xmlDoc, vodItemChild1OfChild1, "key", "DisplayPriority");

                XmlNode vodItemChild2 = addNodeChild(this.xmlDoc, "Period", elementVodItem);
                XmlAttribute vodItemChild2Att1 = creatXmlAtt(this.xmlDoc, vodItemChild2, "start", tempObj.vodItemPeriodStart);
                XmlAttribute vodItemChild2Att2 = creatXmlAtt(this.xmlDoc, vodItemChild2, "end", tempObj.vodItemPeriodEnd);
                #endregion

                #region element Product
                elementProduct = addNodeChild(xmlDoc, "Product", rootNode);
                attProductID = creatXmlAtt(xmlDoc, elementProduct, "id", tempObj.productID);
                attProductAction = creatXmlAtt(xmlDoc, elementProduct, "action", tempObj.productAction);
                attProductCurrency = creatXmlAtt(xmlDoc, elementProduct, "currency", tempObj.productCurrency);
                attProductPrice = creatXmlAtt(xmlDoc, elementProduct, "price", tempObj.productPrice);
                attProductType = creatXmlAtt(xmlDoc, elementProduct, "type", tempObj.productType);
                attProductTitle = creatXmlAtt(xmlDoc, elementProduct, "title", tempObj.productTitle);

                XmlNode productChild1 = addNodeChild(this.xmlDoc, "AddToProduct", elementProduct);
                XmlAttribute productChild1Att1 = creatXmlAtt(this.xmlDoc, productChild1, "elementKind", tempObj.productElementKind);
                XmlAttribute productChild1Att2 = creatXmlAtt(this.xmlDoc, productChild1, "elementId", tempObj.productElementId);
                #endregion               
            }
            catch(Exception ex) { HDControl.HDMessageBox.Show(ex.ToString()); }
        }
        public void SaveXmlFile(string tempPath)
        {
            this.xmlDoc.Save(tempPath);
        }
        private XmlAttribute creatXmlAtt(XmlDocument xmlDocTemp, XmlNode nodeTemp, string nameAtt, string val)
        {
            XmlAttribute attTemp = xmlDocTemp.CreateAttribute(nameAtt);
            attTemp.Value = val;
            nodeTemp.Attributes.Append(attTemp);
            return attTemp;
        }
        private XmlNode addNodeChild(XmlDocument xmlDocTemp, string nodeName, XmlNode parentNode, string innerTxt = "")
        {
            XmlNode nodeTemp = xmlDocTemp.CreateElement(nodeName);
            parentNode.AppendChild(nodeTemp);
            if (innerTxt != "")
            {
                nodeTemp.InnerText = innerTxt;
            }
            return nodeTemp;
        }
    }

    public class XMLLongObject
    {
        XmlDocument xmlDoc = null;
        XmlNode rootNode = null;

        XmlNode elementSeries = null;
        XmlNode elementContent = null;
        XmlNode elementImage = null;
        XmlNode elementImage2 = null;
        XmlNode elementVodItem = null;
        XmlNode elementProduct = null;

        XmlAttribute attRootSchedule = null;
        XmlAttribute attRootID = null;

        XmlAttribute attSeriesID = null;
        XmlAttribute attSeriesTitle = null;
        XmlAttribute attSeriesAction = null;

        XmlAttribute attContentID = null;
        XmlAttribute attContentNumber = null;
        XmlAttribute attContentSeriesRef = null;
        XmlAttribute attContentTitle = null;
        XmlAttribute attContentDuration = null;
        XmlAttribute attContentAction = null;
        XmlAttribute attContentStartDate = null;

        XmlAttribute attImageID = null;
        XmlAttribute attImageTitle = null;
        XmlAttribute attImageIllutratedRef = null;
        XmlAttribute attImageAction = null;
        XmlAttribute attImageStartDate = null;
        XmlAttribute attImageExpiryDate = null;

        XmlAttribute attImage2ID = null;
        XmlAttribute attImage2Title = null;
        XmlAttribute attImage2IllutratedRef = null;
        XmlAttribute attImage2Action = null;
        XmlAttribute attImage2StartDate = null;
        XmlAttribute attImage2ExpiryDate = null;

        XmlAttribute attVodItemID = null;
        XmlAttribute attVodItemContentRef = null;
        XmlAttribute attVodItemNodeRefList = null;
        XmlAttribute attVodItemTitle = null;
        XmlAttribute attVodItemAction = null;

        XmlAttribute attProductID = null;
        XmlAttribute attProductAction = null;
        XmlAttribute attProductCurrency = null;
        XmlAttribute attProductPrice = null;
        XmlAttribute attProductType = null;
        XmlAttribute attProductTitle = null;

        public XMLLongObject()
        {
            xmlDoc = new XmlDocument();
            rootNode = xmlDoc.CreateElement("LysisData");
            xmlDoc.AppendChild(rootNode);

        }
        public void GenerateXml(XMLLongChildObject tempObj)
        {
            try
            {
                attRootSchedule = creatXmlAtt(xmlDoc, rootNode, "scheduleDate", tempObj.rootScheduleDate);
                attRootID = creatXmlAtt(xmlDoc, rootNode, "id", tempObj.rootID);

                #region element Series
                elementSeries = addNodeChild(xmlDoc, "Series", rootNode);
                attSeriesID = creatXmlAtt(xmlDoc, elementSeries, "id", tempObj.seriesID);
                attSeriesTitle = creatXmlAtt(xmlDoc, elementSeries, "title", tempObj.seriesTitle);
                attSeriesAction = creatXmlAtt(xmlDoc, elementSeries, "action", tempObj.seriesAction);

                XmlNode seriesChild1 = addNodeChild(this.xmlDoc, "EpgDescription", elementSeries);
                XmlAttribute seriesChild1Att = creatXmlAtt(this.xmlDoc, seriesChild1, "locale", "vi_VN");
                XmlNode seriesChildOfChild1 = addNodeChild(this.xmlDoc, "EpgElement", seriesChild1, tempObj.seriesViTitle);
                XmlAttribute seriesChildOfChild1Att = creatXmlAtt(this.xmlDoc, seriesChildOfChild1, "key", "Title");
                XmlNode seriesChildOfChild2 = addNodeChild(this.xmlDoc, "EpgElement", seriesChild1, tempObj.seriesViSynopsis);
                XmlAttribute seriesChildOfChild2Att = creatXmlAtt(this.xmlDoc, seriesChildOfChild2, "key", "Synopsis");

                XmlNode seriesChild2 = addNodeChild(this.xmlDoc, "EpgDescription", elementSeries);
                XmlAttribute seriesChild2Att = creatXmlAtt(this.xmlDoc, seriesChild2, "locale", "en_GB");
                XmlNode seriesChild2OfChild1 = addNodeChild(this.xmlDoc, "EpgElement", seriesChild2, tempObj.seriesEnTitle);
                XmlAttribute seriesChild2OfChild1Att = creatXmlAtt(this.xmlDoc, seriesChild2OfChild1, "key", "Title");

                XmlNode seriesChild3 = addNodeChild(this.xmlDoc, "EpgDescription", elementSeries);
                XmlNode seriesChild3OfChild1 = addNodeChild(this.xmlDoc, "EpgElement", seriesChild3, tempObj.seriesPromoImages);
                XmlAttribute seriesChild3OfChild1Att = creatXmlAtt(this.xmlDoc, seriesChild3OfChild1, "key", "PromoImages");
                XmlNode seriesChild3OfChild2 = addNodeChild(this.xmlDoc, "EpgElement", seriesChild3, tempObj.seriesCategories);
                XmlAttribute seriesChild3OfChild2Att = creatXmlAtt(this.xmlDoc, seriesChild3OfChild2, "key", "Categories");
                XmlNode seriesChild3OfChild3 = addNodeChild(this.xmlDoc, "EpgElement", seriesChild3, tempObj.seriesRating);
                XmlAttribute seriesChild3OfChild3Att = creatXmlAtt(this.xmlDoc, seriesChild3OfChild3, "key", "Rating");
                #endregion

                #region element Content
                elementContent = addNodeChild(xmlDoc, "Content", rootNode);
                attContentID = creatXmlAtt(xmlDoc, elementContent, "id", tempObj.contentID);
                attContentNumber = creatXmlAtt(xmlDoc, elementContent, "number", tempObj.contentNumber);
                attContentSeriesRef = creatXmlAtt(xmlDoc, elementContent, "seriesRef", tempObj.contentSeriesRef);
                attContentTitle = creatXmlAtt(xmlDoc, elementContent, "title", tempObj.contentTitle);
                attContentDuration = creatXmlAtt(xmlDoc, elementContent, "duration", tempObj.contentDuration);
                attContentAction = creatXmlAtt(xmlDoc, elementContent, "action", tempObj.contentAction);
                attContentStartDate = creatXmlAtt(xmlDoc, elementContent, "startDate", tempObj.contentStartDate);

                XmlNode contentChild1 = addNodeChild(this.xmlDoc, "EpgDescription", elementContent);
                XmlAttribute contentChild1Att = creatXmlAtt(this.xmlDoc, contentChild1, "locale", "vi_VN");
                XmlNode contentChild1OfChild1 = addNodeChild(this.xmlDoc, "EpgElement", contentChild1, tempObj.contentViTitle);
                XmlAttribute contentChild1OfChild1Att = creatXmlAtt(this.xmlDoc, contentChild1OfChild1, "key", "Title");
                XmlNode contentChild1OfChild2 = addNodeChild(this.xmlDoc, "EpgElement", contentChild1, tempObj.contentViDescription);
                XmlAttribute contentChild1OfChild2Att = creatXmlAtt(this.xmlDoc, contentChild1OfChild2, "key", "Description");
                XmlNode contentChild1OfChild3 = addNodeChild(this.xmlDoc, "EpgElement", contentChild1, tempObj.contentViCopyright);
                XmlAttribute contentChild1OfChild3Att = creatXmlAtt(this.xmlDoc, contentChild1OfChild3, "key", "Copyright");
                XmlNode contentChild1OfChild4 = addNodeChild(this.xmlDoc, "EpgElement", contentChild1, tempObj.contentViSynopsis);
                XmlAttribute contentChild1OfChild4Att = creatXmlAtt(this.xmlDoc, contentChild1OfChild4, "key", "Synopsis");

                XmlNode contentChild2 = addNodeChild(this.xmlDoc, "EpgDescription", elementContent);
                XmlNode contentChild2OfChild1 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentPromoImages);
                XmlAttribute contentChild2OfChild1Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild1, "key", "PromoImages");
                XmlNode contentChild2OfChild2 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentAspect);
                XmlAttribute contentChild2OfChild2Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild2, "key", "Aspect");
                XmlNode contentChild2OfChild3 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentIsRecordable);
                XmlAttribute contentChild2OfChild3Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild3, "key", "IsRecordable");
                XmlNode contentChild2OfChild4 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentYear);
                XmlAttribute contentChild2OfChild4Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild4, "key", "Year");
                XmlNode contentChild2OfChild5 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentRating);
                XmlAttribute contentChild2OfChild5Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild5, "key", "Rating");
                XmlNode contentChild2OfChild6 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentCategories);
                XmlAttribute contentChild2OfChild6Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild6, "key", "Categories");
                XmlNode contentChild2OfChild7 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentActors);
                XmlAttribute contentChild2OfChild7Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild7, "key", "Actors");
                XmlNode contentChild2OfChild8 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentDirectors);
                XmlAttribute contentChild2OfChild8Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild8, "key", "Directors");
                XmlNode contentChild2OfChild9 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentLanguage);
                XmlAttribute contentChild2OfChild9Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild9, "key", "Language");
                XmlNode contentChild2OfChild10 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentSubtitles);
                XmlAttribute contentChild2OfChild10Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild10, "key", "Subtitles");
                XmlNode contentChild2OfChild11 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentStudio);
                XmlAttribute contentChild2OfChild11Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild11, "key", "Studio");
                XmlNode contentChild2OfChild12 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentCountries);
                XmlAttribute contentChild2OfChild12Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild12, "key", "Countries");
                XmlNode contentChild2OfChild13 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentServiceId);
                XmlAttribute contentChild2OfChild13Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild13, "key", "ServiceId");

                XmlNode contentChild3 = addNodeChild(this.xmlDoc, "Media", elementContent);
                XmlAttribute contentChild3Att1 = creatXmlAtt(this.xmlDoc, contentChild3, "id", tempObj.contentMediaID);
                XmlAttribute contentChild3Att2 = creatXmlAtt(this.xmlDoc, contentChild3, "fileName", tempObj.contentMediaFilename);
                XmlAttribute contentChild3Att3 = creatXmlAtt(this.xmlDoc, contentChild3, "fileSize", tempObj.contentMediaFileSize);
                XmlAttribute contentChild3Att4 = creatXmlAtt(this.xmlDoc, contentChild3, "format", tempObj.contentMediaFormat);
                XmlAttribute contentChild3Att5 = creatXmlAtt(this.xmlDoc, contentChild3, "frameDuration", tempObj.contentMediaFrameDuration);
                XmlAttribute contentChild3Att6 = creatXmlAtt(this.xmlDoc, contentChild3, "srcAssetType", tempObj.contentMediaSrcAssetType);
                XmlAttribute contentChild3Att7 = creatXmlAtt(this.xmlDoc, contentChild3, "action", tempObj.contentMediaAction);

                XmlNode contentChild3OfChild = addNodeChild(this.xmlDoc, "AssetDeviceLink", contentChild3);
                XmlAttribute contentChild3OfChild1Att = creatXmlAtt(this.xmlDoc, contentChild3OfChild, "storageDevice", tempObj.contentMediaStorageDevice);
                XmlAttribute contentChild3OfChild2Att = creatXmlAtt(this.xmlDoc, contentChild3OfChild, "type", tempObj.contentMediaType);
                XmlAttribute contentChild3OfChild3Att = creatXmlAtt(this.xmlDoc, contentChild3OfChild, "relativePath", tempObj.contentMediaRelativePath);

                XmlNode contentChild4 = addNodeChild(this.xmlDoc, "Content", elementContent);
                XmlAttribute contentChild4Att1 = creatXmlAtt(this.xmlDoc, contentChild4, "id", tempObj.contentContentID);
                XmlAttribute contentChild4Att2 = creatXmlAtt(this.xmlDoc, contentChild4, "title", tempObj.contentContentTitle);
                XmlAttribute contentChild4Att3 = creatXmlAtt(this.xmlDoc, contentChild4, "preLoaded", tempObj.contentContentPreLoaded);
                XmlAttribute contentChild4Att4 = creatXmlAtt(this.xmlDoc, contentChild4, "startDate", tempObj.contentContentStartDate);
                XmlAttribute contentChild4Att5 = creatXmlAtt(this.xmlDoc, contentChild4, "encProfileName", tempObj.contentContentEncProfileName);
                XmlAttribute contentChild4Att6 = creatXmlAtt(this.xmlDoc, contentChild4, "action", tempObj.contentContentAction);

                XmlNode contentChild4OfChild = addNodeChild(this.xmlDoc, "EpgDescription", contentChild4);
                XmlNode contentChild4OfChildOfChild = addNodeChild(this.xmlDoc, "EpgElement", contentChild4OfChild, tempObj.contentContentDefinition);
                XmlAttribute contentChild4OfChildOfChildAtt = creatXmlAtt(this.xmlDoc, contentChild4OfChildOfChild, "key", "Definition");

                #endregion

                #region element Image
                elementImage = addNodeChild(xmlDoc, "Image", rootNode);
                attImageID = creatXmlAtt(xmlDoc, elementImage, "id", tempObj.imageID);
                attImageTitle = creatXmlAtt(xmlDoc, elementImage, "title", tempObj.imageTitle);
                attImageIllutratedRef = creatXmlAtt(xmlDoc, elementImage, "illustratedRef", tempObj.imageIllustratedRef);
                attImageAction = creatXmlAtt(xmlDoc, elementImage, "action", tempObj.imageAction);
                attImageStartDate = creatXmlAtt(xmlDoc, elementImage, "startDate", tempObj.imageStartDate);
                attImageExpiryDate = creatXmlAtt(xmlDoc, elementImage, "expiryDate", tempObj.imageExpiryDate);

                XmlNode imageChild1 = addNodeChild(this.xmlDoc, "Media", elementImage);
                XmlAttribute imageChild1Att1 = creatXmlAtt(this.xmlDoc, imageChild1, "id", tempObj.imageMediaID);
                XmlAttribute imageChild1Att2 = creatXmlAtt(this.xmlDoc, imageChild1, "fileName", tempObj.imageMediaFileName);
                XmlAttribute imageChild1Att3 = creatXmlAtt(this.xmlDoc, imageChild1, "srcAssetType", tempObj.imageMediaSrcAssetType);
                XmlAttribute imageChild1Att4 = creatXmlAtt(this.xmlDoc, imageChild1, "frameDuration", tempObj.imageMediaFrameDuration);
                XmlAttribute imageChild1Att5 = creatXmlAtt(this.xmlDoc, imageChild1, "fileSize", tempObj.imageMediaFileSize);
                XmlAttribute imageChild1Att6 = creatXmlAtt(this.xmlDoc, imageChild1, "comment", tempObj.imageMediaComment);
                XmlAttribute imageChild1Att7 = creatXmlAtt(this.xmlDoc, imageChild1, "format", tempObj.imageMediaFormat);

                XmlNode imageChild1OfChild = addNodeChild(this.xmlDoc, "AssetDeviceLink", imageChild1);
                XmlAttribute imageChild1OfChildAtt = creatXmlAtt(this.xmlDoc, imageChild1OfChild, "storageDevice", tempObj.imageMediaStorageDevice);
                XmlAttribute imageChild1OfChildAtt2 = creatXmlAtt(this.xmlDoc, imageChild1OfChild, "type", tempObj.imageMediaType);
                XmlAttribute imageChild1OfChildAtt3 = creatXmlAtt(this.xmlDoc, imageChild1OfChild, "relativePath", tempObj.imageMediaRelativePath);

                XmlNode imageChild2 = addNodeChild(this.xmlDoc, "Image", elementImage);
                XmlAttribute imageChild2Att1 = creatXmlAtt(this.xmlDoc, imageChild2, "id", tempObj.imageImageID);
                XmlAttribute imageChild2Att2 = creatXmlAtt(this.xmlDoc, imageChild2, "title", tempObj.imageImageTitle);
                XmlAttribute imageChild2Att3 = creatXmlAtt(this.xmlDoc, imageChild2, "action", tempObj.imageImageAction);
                XmlAttribute imageChild2Att4 = creatXmlAtt(this.xmlDoc, imageChild2, "startDate", tempObj.imageImageStartdate);
                XmlAttribute imageChild2Att5 = creatXmlAtt(this.xmlDoc, imageChild2, "expiryDate", tempObj.imageImageExpiryDate);
                XmlAttribute imageChild2Att6 = creatXmlAtt(this.xmlDoc, imageChild2, "encProfileName", tempObj.imageImageEncProfileName);
                XmlAttribute imageChild2Att7 = creatXmlAtt(this.xmlDoc, imageChild2, "preLoaded", tempObj.imageImagePreLoaded);
                #endregion

                #region element Image2
                elementImage2 = addNodeChild(xmlDoc, "Image", rootNode);
                attImage2ID = creatXmlAtt(xmlDoc, elementImage2, "id", tempObj.image2ID);
                attImage2Title = creatXmlAtt(xmlDoc, elementImage2, "title", tempObj.image2Title);
                attImage2IllutratedRef = creatXmlAtt(xmlDoc, elementImage2, "illustratedRef", tempObj.image2IllustratedRef);
                attImage2Action = creatXmlAtt(xmlDoc, elementImage2, "action", tempObj.image2Action);
                attImage2StartDate = creatXmlAtt(xmlDoc, elementImage2, "startDate", tempObj.image2StartDate);
                attImage2ExpiryDate = creatXmlAtt(xmlDoc, elementImage2, "expiryDate", tempObj.image2ExpiryDate);

                XmlNode image2Child1 = addNodeChild(this.xmlDoc, "Media", elementImage2);
                XmlAttribute image2Child1Att1 = creatXmlAtt(this.xmlDoc, image2Child1, "id", tempObj.image2MediaID);
                XmlAttribute image2Child1Att2 = creatXmlAtt(this.xmlDoc, image2Child1, "fileName", tempObj.image2MediaFileName);
                XmlAttribute image2Child1Att3 = creatXmlAtt(this.xmlDoc, image2Child1, "srcAssetType", tempObj.image2MediaSrcAssetType);
                XmlAttribute image2Child1Att4 = creatXmlAtt(this.xmlDoc, image2Child1, "frameDuration", tempObj.image2MediaFrameDuration);
                XmlAttribute image2Child1Att5 = creatXmlAtt(this.xmlDoc, image2Child1, "fileSize", tempObj.image2MediaFileSize);
                XmlAttribute image2Child1Att6 = creatXmlAtt(this.xmlDoc, image2Child1, "comment", tempObj.image2MediaComment);
                XmlAttribute image2Child1Att7 = creatXmlAtt(this.xmlDoc, image2Child1, "format", tempObj.image2MediaFormat);

                XmlNode image2Child1OfChild = addNodeChild(this.xmlDoc, "AssetDeviceLink", image2Child1);
                XmlAttribute image2Child1OfChildAtt = creatXmlAtt(this.xmlDoc, image2Child1OfChild, "storageDevice", tempObj.image2MediaStorageDevice);
                XmlAttribute image2Child1OfChildAtt2 = creatXmlAtt(this.xmlDoc, image2Child1OfChild, "type", tempObj.image2MediaType);
                XmlAttribute image2Child1OfChildAtt3 = creatXmlAtt(this.xmlDoc, image2Child1OfChild, "relativePath", tempObj.image2MediaRelativePath);

                XmlNode image2Child2 = addNodeChild(this.xmlDoc, "Image", elementImage2);
                XmlAttribute image2Child2Att1 = creatXmlAtt(this.xmlDoc, image2Child2, "id", tempObj.image2ImageID);
                XmlAttribute image2Child2Att2 = creatXmlAtt(this.xmlDoc, image2Child2, "title", tempObj.image2ImageTitle);
                XmlAttribute image2Child2Att3 = creatXmlAtt(this.xmlDoc, image2Child2, "action", tempObj.image2ImageAction);
                XmlAttribute image2Child2Att4 = creatXmlAtt(this.xmlDoc, image2Child2, "startDate", tempObj.image2ImageStartdate);
                XmlAttribute image2Child2Att5 = creatXmlAtt(this.xmlDoc, image2Child2, "expiryDate", tempObj.image2ImageExpiryDate);
                XmlAttribute image2Child2Att6 = creatXmlAtt(this.xmlDoc, image2Child2, "encProfileName", tempObj.image2ImageEncProfileName);
                XmlAttribute image2Child2Att7 = creatXmlAtt(this.xmlDoc, image2Child2, "preLoaded", tempObj.image2ImagePreLoaded);
                #endregion

                #region element VodItem
                elementVodItem = addNodeChild(xmlDoc, "VodItem", rootNode);
                attVodItemID = creatXmlAtt(xmlDoc, elementVodItem, "id", tempObj.vodItemID);
                attVodItemContentRef = creatXmlAtt(xmlDoc, elementVodItem, "contentRef", tempObj.vodItemContentRef);
                attVodItemNodeRefList = creatXmlAtt(xmlDoc, elementVodItem, "nodeRefList", tempObj.vodItemNodeRefList);
                attVodItemTitle = creatXmlAtt(xmlDoc, elementVodItem, "title", tempObj.vodItemTitle);
                attVodItemAction = creatXmlAtt(xmlDoc, elementVodItem, "action", tempObj.vodItemAction);

                XmlNode vodItemChild1 = addNodeChild(this.xmlDoc, "EpgDescription", elementVodItem);
                XmlNode vodItemChild1OfChild1 = addNodeChild(this.xmlDoc, "EpgElement", vodItemChild1, tempObj.vodItemDisplayPriority);
                XmlAttribute vodItemChild1OfChild1Att1 = creatXmlAtt(this.xmlDoc, vodItemChild1OfChild1, "key", "DisplayPriority");

                XmlNode vodItemChild2 = addNodeChild(this.xmlDoc, "Period", elementVodItem);
                XmlAttribute vodItemChild2Att1 = creatXmlAtt(this.xmlDoc, vodItemChild2, "start", tempObj.vodItemPeriodStart);
                XmlAttribute vodItemChild2Att2 = creatXmlAtt(this.xmlDoc, vodItemChild2, "end", tempObj.vodItemPeriodEnd);
                #endregion

                #region element Product
                elementProduct = addNodeChild(xmlDoc, "Product", rootNode);
                attProductID = creatXmlAtt(xmlDoc, elementProduct, "id", tempObj.productID);
                attProductAction = creatXmlAtt(xmlDoc, elementProduct, "action", tempObj.productAction);
                attProductCurrency = creatXmlAtt(xmlDoc, elementProduct, "currency", tempObj.productCurrency);
                attProductPrice = creatXmlAtt(xmlDoc, elementProduct, "price", tempObj.productPrice);
                attProductType = creatXmlAtt(xmlDoc, elementProduct, "type", tempObj.productType);
                attProductTitle = creatXmlAtt(xmlDoc, elementProduct, "title", tempObj.productTitle);

                XmlNode productChild1 = addNodeChild(this.xmlDoc, "AddToProduct", elementProduct);
                XmlAttribute productChild1Att1 = creatXmlAtt(this.xmlDoc, productChild1, "elementKind", tempObj.productElementKind);
                XmlAttribute productChild1Att2 = creatXmlAtt(this.xmlDoc, productChild1, "elementId", tempObj.productElementId);
                #endregion               
            }
            catch (Exception ex) { HDControl.HDMessageBox.Show(ex.ToString()); }
        }
        public void SaveXmlFile(string tempPath)
        {
            this.xmlDoc.Save(tempPath);
        }
        private XmlAttribute creatXmlAtt(XmlDocument xmlDocTemp, XmlNode nodeTemp, string nameAtt, string val)
        {
            XmlAttribute attTemp = xmlDocTemp.CreateAttribute(nameAtt);
            attTemp.Value = val;
            nodeTemp.Attributes.Append(attTemp);
            return attTemp;
        }
        private XmlNode addNodeChild(XmlDocument xmlDocTemp, string nodeName, XmlNode parentNode, string innerTxt = "")
        {
            XmlNode nodeTemp = xmlDocTemp.CreateElement(nodeName);
            parentNode.AppendChild(nodeTemp);
            if (innerTxt != "")
            {
                nodeTemp.InnerText = innerTxt;
            }
            return nodeTemp;
        }
    }

    public class XMLShortObject
    {
        XmlDocument xmlDoc = null;
        XmlNode rootNode = null;

        XmlNode elementContent = null;        
        XmlNode elementVodItem = null;
        XmlNode elementProduct = null;

        XmlAttribute attRootSchedule = null;
        XmlAttribute attRootID = null;

        XmlAttribute attContentID = null;
        XmlAttribute attContentNumber = null;
        XmlAttribute attContentSeriesRef = null;
        XmlAttribute attContentTitle = null;
        XmlAttribute attContentDuration = null;
        XmlAttribute attContentAction = null;
        XmlAttribute attContentStartDate = null;               

        XmlAttribute attVodItemID = null;
        XmlAttribute attVodItemContentRef = null;
        XmlAttribute attVodItemNodeRefList = null;
        XmlAttribute attVodItemTitle = null;
        XmlAttribute attVodItemAction = null;

        XmlAttribute attProductID = null;
        XmlAttribute attProductAction = null;
        XmlAttribute attProductCurrency = null;
        XmlAttribute attProductPrice = null;
        XmlAttribute attProductType = null;
        XmlAttribute attProductTitle = null;

        public XMLShortObject()
        {
            xmlDoc = new XmlDocument();
            rootNode = xmlDoc.CreateElement("LysisData");
            xmlDoc.AppendChild(rootNode);

        }
        public void GenerateXml(XMLShortChildObject tempObj)
        {
            try
            {
                attRootSchedule = creatXmlAtt(xmlDoc, rootNode, "scheduleDate", tempObj.rootScheduleDate);
                attRootID = creatXmlAtt(xmlDoc, rootNode, "id", tempObj.rootID);

                #region element Content
                elementContent = addNodeChild(xmlDoc, "Content", rootNode);
                attContentID = creatXmlAtt(xmlDoc, elementContent, "id", tempObj.contentID);
                attContentNumber = creatXmlAtt(xmlDoc, elementContent, "number", tempObj.contentNumber);
                attContentSeriesRef = creatXmlAtt(xmlDoc, elementContent, "seriesRef", tempObj.contentSeriesRef);
                attContentTitle = creatXmlAtt(xmlDoc, elementContent, "title", tempObj.contentTitle);
                attContentDuration = creatXmlAtt(xmlDoc, elementContent, "duration", tempObj.contentDuration);
                attContentAction = creatXmlAtt(xmlDoc, elementContent, "action", tempObj.contentAction);
                attContentStartDate = creatXmlAtt(xmlDoc, elementContent, "startDate", tempObj.contentStartDate);

                XmlNode contentChild1 = addNodeChild(this.xmlDoc, "EpgDescription", elementContent);
                XmlAttribute contentChild1Att = creatXmlAtt(this.xmlDoc, contentChild1, "locale", "vi_VN");
                XmlNode contentChild1OfChild1 = addNodeChild(this.xmlDoc, "EpgElement", contentChild1, tempObj.contentViTitle);
                XmlAttribute contentChild1OfChild1Att = creatXmlAtt(this.xmlDoc, contentChild1OfChild1, "key", "Title");
                XmlNode contentChild1OfChild2 = addNodeChild(this.xmlDoc, "EpgElement", contentChild1, tempObj.contentViDescription);
                XmlAttribute contentChild1OfChild2Att = creatXmlAtt(this.xmlDoc, contentChild1OfChild2, "key", "Description");
                XmlNode contentChild1OfChild3 = addNodeChild(this.xmlDoc, "EpgElement", contentChild1, tempObj.contentViCopyright);
                XmlAttribute contentChild1OfChild3Att = creatXmlAtt(this.xmlDoc, contentChild1OfChild3, "key", "Copyright");
                XmlNode contentChild1OfChild4 = addNodeChild(this.xmlDoc, "EpgElement", contentChild1, tempObj.contentViSynopsis);
                XmlAttribute contentChild1OfChild4Att = creatXmlAtt(this.xmlDoc, contentChild1OfChild4, "key", "Synopsis");

                XmlNode contentChild2 = addNodeChild(this.xmlDoc, "EpgDescription", elementContent);
                XmlNode contentChild2OfChild1 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentPromoImages);
                XmlAttribute contentChild2OfChild1Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild1, "key", "PromoImages");
                XmlNode contentChild2OfChild2 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentAspect);
                XmlAttribute contentChild2OfChild2Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild2, "key", "Aspect");
                XmlNode contentChild2OfChild3 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentIsRecordable);
                XmlAttribute contentChild2OfChild3Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild3, "key", "IsRecordable");
                XmlNode contentChild2OfChild4 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentYear);
                XmlAttribute contentChild2OfChild4Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild4, "key", "Year");
                XmlNode contentChild2OfChild5 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentRating);
                XmlAttribute contentChild2OfChild5Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild5, "key", "Rating");
                XmlNode contentChild2OfChild6 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentCategories);
                XmlAttribute contentChild2OfChild6Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild6, "key", "Categories");
                XmlNode contentChild2OfChild7 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentActors);
                XmlAttribute contentChild2OfChild7Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild7, "key", "Actors");
                XmlNode contentChild2OfChild8 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentDirectors);
                XmlAttribute contentChild2OfChild8Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild8, "key", "Directors");
                XmlNode contentChild2OfChild9 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentLanguage);
                XmlAttribute contentChild2OfChild9Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild9, "key", "Language");
                XmlNode contentChild2OfChild10 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentSubtitles);
                XmlAttribute contentChild2OfChild10Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild10, "key", "Subtitles");
                XmlNode contentChild2OfChild11 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentStudio);
                XmlAttribute contentChild2OfChild11Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild11, "key", "Studio");
                XmlNode contentChild2OfChild12 = addNodeChild(this.xmlDoc, "EpgElement", contentChild2, tempObj.contentCountries);
                XmlAttribute contentChild2OfChild12Att = creatXmlAtt(this.xmlDoc, contentChild2OfChild12, "key", "Countries");

                XmlNode contentChild3 = addNodeChild(this.xmlDoc, "Media", elementContent);
                XmlAttribute contentChild3Att1 = creatXmlAtt(this.xmlDoc, contentChild3, "id", tempObj.contentMediaID);
                XmlAttribute contentChild3Att2 = creatXmlAtt(this.xmlDoc, contentChild3, "fileName", tempObj.contentMediaFilename);
                XmlAttribute contentChild3Att3 = creatXmlAtt(this.xmlDoc, contentChild3, "fileSize", tempObj.contentMediaFileSize);
                XmlAttribute contentChild3Att4 = creatXmlAtt(this.xmlDoc, contentChild3, "format", tempObj.contentMediaFormat);
                XmlAttribute contentChild3Att5 = creatXmlAtt(this.xmlDoc, contentChild3, "frameDuration", tempObj.contentMediaFrameDuration);
                XmlAttribute contentChild3Att6 = creatXmlAtt(this.xmlDoc, contentChild3, "srcAssetType", tempObj.contentMediaSrcAssetType);
                XmlAttribute contentChild3Att7 = creatXmlAtt(this.xmlDoc, contentChild3, "action", tempObj.contentMediaAction);

                XmlNode contentChild3OfChild = addNodeChild(this.xmlDoc, "AssetDeviceLink", contentChild3);
                XmlAttribute contentChild3OfChild1Att = creatXmlAtt(this.xmlDoc, contentChild3OfChild, "storageDevice", tempObj.contentMediaStorageDevice);
                XmlAttribute contentChild3OfChild2Att = creatXmlAtt(this.xmlDoc, contentChild3OfChild, "type", tempObj.contentMediaType);
                XmlAttribute contentChild3OfChild3Att = creatXmlAtt(this.xmlDoc, contentChild3OfChild, "relativePath", tempObj.contentMediaRelativePath);

                XmlNode contentChild4 = addNodeChild(this.xmlDoc, "Content", elementContent);
                XmlAttribute contentChild4Att1 = creatXmlAtt(this.xmlDoc, contentChild4, "id", tempObj.contentContentID);
                XmlAttribute contentChild4Att2 = creatXmlAtt(this.xmlDoc, contentChild4, "title", tempObj.contentContentTitle);
                XmlAttribute contentChild4Att3 = creatXmlAtt(this.xmlDoc, contentChild4, "preLoaded", tempObj.contentContentPreLoaded);
                XmlAttribute contentChild4Att4 = creatXmlAtt(this.xmlDoc, contentChild4, "startDate", tempObj.contentContentStartDate);
                XmlAttribute contentChild4Att5 = creatXmlAtt(this.xmlDoc, contentChild4, "encProfileName", tempObj.contentContentEncProfileName);
                XmlAttribute contentChild4Att6 = creatXmlAtt(this.xmlDoc, contentChild4, "action", tempObj.contentContentAction);

                XmlNode contentChild4OfChild = addNodeChild(this.xmlDoc, "EpgDescription", contentChild4);
                XmlNode contentChild4OfChildOfChild = addNodeChild(this.xmlDoc, "EpgElement", contentChild4OfChild, tempObj.contentContentDefinition);
                XmlAttribute contentChild4OfChildOfChildAtt = creatXmlAtt(this.xmlDoc, contentChild4OfChildOfChild, "key", "Definition");

                #endregion                

                #region element VodItem
                elementVodItem = addNodeChild(xmlDoc, "VodItem", rootNode);
                attVodItemID = creatXmlAtt(xmlDoc, elementVodItem, "id", tempObj.vodItemID);
                attVodItemContentRef = creatXmlAtt(xmlDoc, elementVodItem, "contentRef", tempObj.vodItemContentRef);
                attVodItemNodeRefList = creatXmlAtt(xmlDoc, elementVodItem, "nodeRefList", tempObj.vodItemNodeRefList);
                attVodItemTitle = creatXmlAtt(xmlDoc, elementVodItem, "title", tempObj.vodItemTitle);
                attVodItemAction = creatXmlAtt(xmlDoc, elementVodItem, "action", tempObj.vodItemAction);

                XmlNode vodItemChild1 = addNodeChild(this.xmlDoc, "EpgDescription", elementVodItem);
                XmlNode vodItemChild1OfChild1 = addNodeChild(this.xmlDoc, "EpgElement", vodItemChild1, tempObj.vodItemDisplayPriority);
                XmlAttribute vodItemChild1OfChild1Att1 = creatXmlAtt(this.xmlDoc, vodItemChild1OfChild1, "key", "DisplayPriority");

                XmlNode vodItemChild2 = addNodeChild(this.xmlDoc, "Period", elementVodItem);
                XmlAttribute vodItemChild2Att1 = creatXmlAtt(this.xmlDoc, vodItemChild2, "start", tempObj.vodItemPeriodStart);
                XmlAttribute vodItemChild2Att2 = creatXmlAtt(this.xmlDoc, vodItemChild2, "end", tempObj.vodItemPeriodEnd);
                #endregion

                #region element Product
                elementProduct = addNodeChild(xmlDoc, "Product", rootNode);
                attProductID = creatXmlAtt(xmlDoc, elementProduct, "id", tempObj.productID);
                attProductAction = creatXmlAtt(xmlDoc, elementProduct, "action", tempObj.productAction);
                attProductCurrency = creatXmlAtt(xmlDoc, elementProduct, "currency", tempObj.productCurrency);
                attProductPrice = creatXmlAtt(xmlDoc, elementProduct, "price", tempObj.productPrice);
                attProductType = creatXmlAtt(xmlDoc, elementProduct, "type", tempObj.productType);
                attProductTitle = creatXmlAtt(xmlDoc, elementProduct, "title", tempObj.productTitle);

                XmlNode productChild1 = addNodeChild(this.xmlDoc, "AddToProduct", elementProduct);
                XmlAttribute productChild1Att1 = creatXmlAtt(this.xmlDoc, productChild1, "elementKind", tempObj.productElementKind);
                XmlAttribute productChild1Att2 = creatXmlAtt(this.xmlDoc, productChild1, "elementId", tempObj.productElementId);
                #endregion               
            }
            catch (Exception ex) { HDControl.HDMessageBox.Show(ex.ToString()); }
        }
        public void SaveXmlFile(string tempPath)
        {
            this.xmlDoc.Save(tempPath);
        }
        private XmlAttribute creatXmlAtt(XmlDocument xmlDocTemp, XmlNode nodeTemp, string nameAtt, string val)
        {
            XmlAttribute attTemp = xmlDocTemp.CreateAttribute(nameAtt);
            attTemp.Value = val;
            nodeTemp.Attributes.Append(attTemp);
            return attTemp;
        }
        private XmlNode addNodeChild(XmlDocument xmlDocTemp, string nodeName, XmlNode parentNode, string innerTxt = "")
        {
            XmlNode nodeTemp = xmlDocTemp.CreateElement(nodeName);
            parentNode.AppendChild(nodeTemp);
            if (innerTxt != "")
            {
                nodeTemp.InnerText = innerTxt;
            }
            return nodeTemp;
        }
    }
}
