using DTO;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ClientWeb
{
    public class AddressSearchHelper
    {
        public static void Create(List<AddressSearchItem> lst)
        {
            string path = HttpContext.Current.Server.MapPath("/" + FolderUpload.Address);
            var dir = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(path));
            if (IndexWriter.IsLocked(dir))
                IndexWriter.Unlock(dir);
            var lockFilePath = Path.Combine(path, "write.lock");
            if (File.Exists(lockFilePath))
                File.Delete(lockFilePath);
            var indexWriter = new IndexWriter(dir, new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29), true, IndexWriter.MaxFieldLength.LIMITED);
            try
            {
                foreach (var item in lst)
                {
                    if (item.CustomerID > 0 && item.CUSLocationID > 0 && !string.IsNullOrEmpty(item.Address) && !string.IsNullOrEmpty(item.PartnerCode) && !string.IsNullOrEmpty(item.LocationCode))
                    {
                        var bookDocument = new Document();
                        bookDocument.Add(new Field("CustomerID", item.CustomerID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
                        int parnerid = item.CUSPartnerID > 0 ? item.CUSPartnerID.Value : -1;
                        bookDocument.Add(new Field("CUSPartnerID", parnerid.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
                        bookDocument.Add(new Field("CUSLocationID", item.CUSLocationID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
                        bookDocument.Add(new Field("PartnerCode", item.PartnerCode.Trim(), Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
                        bookDocument.Add(new Field("LocationCode", item.LocationCode.Trim(), Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
                        string strEconomicZone = item.EconomicZone == null ? "" : item.EconomicZone;
                        bookDocument.Add(new Field("EconomicZone", strEconomicZone.Trim().ToLower(), Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
                        bookDocument.Add(new Field("Address", item.Address.Trim(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
                        indexWriter.AddDocument(bookDocument);
                    }
                }
                indexWriter.Optimize();
                indexWriter.Commit();
            }
            finally
            {
                indexWriter.Dispose();
            }
        }

        public static void Update(AddressSearchItem item)
        {
            string path = HttpContext.Current.Server.MapPath("/" + FolderUpload.Address);
            Delete(item);
            using (var indexWriter = new IndexWriter(Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(path)), new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29), false, IndexWriter.MaxFieldLength.LIMITED))
            {
                if (item != null && item.CustomerID > 0 && item.CUSLocationID > 0 && !string.IsNullOrEmpty(item.Address))
                {
                    var bookDocument = new Document();
                    bookDocument.Add(new Field("CustomerID", item.CustomerID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
                    int parnerid = item.CUSPartnerID > 0 ? item.CUSPartnerID.Value : -1;
                    bookDocument.Add(new Field("CUSPartnerID", parnerid.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
                    bookDocument.Add(new Field("CUSLocationID", item.CUSLocationID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
                    bookDocument.Add(new Field("PartnerCode", item.PartnerCode.Trim(), Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
                    bookDocument.Add(new Field("LocationCode", item.LocationCode.Trim(), Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
                    string strEconomicZone = item.EconomicZone == null ? "" : item.EconomicZone;
                    bookDocument.Add(new Field("EconomicZone", strEconomicZone.Trim().ToLower(), Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
                    bookDocument.Add(new Field("Address", item.Address.Trim(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
                    indexWriter.AddDocument(bookDocument);
                }
                indexWriter.Optimize();
                indexWriter.Commit();
            }
        }

        public static void Delete(AddressSearchItem item)
        {
            string path = HttpContext.Current.Server.MapPath("/" + FolderUpload.Address);
            var indexWriter = new IndexWriter(Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(path)), new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29), false, IndexWriter.MaxFieldLength.LIMITED);
            try
            {
                if (item != null && item.CUSLocationID > 0)
                {
                    indexWriter.DeleteDocuments(new Lucene.Net.Index.Term("CUSLocationID", item.CUSLocationID.ToString()));
                }
                indexWriter.Optimize();
                indexWriter.Commit();
            }
            finally
            {
                indexWriter.Close();
            }
        }

        public static void DeleteByCustomerID(int customerid)
        {
            string path = HttpContext.Current.Server.MapPath("/" + FolderUpload.Address);
            var indexWriter = new IndexWriter(Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(path)), new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29), false, IndexWriter.MaxFieldLength.LIMITED);
            try
            {
                if (customerid > 0)
                {
                    indexWriter.DeleteDocuments(new Lucene.Net.Index.Term("CustomerID", customerid.ToString()));
                }
                indexWriter.Optimize();
                indexWriter.Commit();
            }
            finally
            {
                indexWriter.Close();
            }
        }

        public static void AddListByCustomerID(int customerid, List<AddressSearchItem> lst)
        {
            string path = HttpContext.Current.Server.MapPath("/" + FolderUpload.Address);
            if (customerid > 0)
            {
                DeleteByCustomerID(customerid);
                using (var indexWriter = new IndexWriter(Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(path)), new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29), false, IndexWriter.MaxFieldLength.LIMITED))
                {
                    foreach (var item in lst)
                    {
                        var bookDocument = new Document();
                        bookDocument.Add(new Field("CustomerID", customerid.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
                        int parnerid = item.CUSPartnerID > 0 ? item.CUSPartnerID.Value : -1;
                        bookDocument.Add(new Field("CUSPartnerID", parnerid.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
                        bookDocument.Add(new Field("CUSLocationID", item.CUSLocationID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
                        bookDocument.Add(new Field("PartnerCode", item.PartnerCode.Trim(), Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
                        bookDocument.Add(new Field("LocationCode", item.LocationCode.Trim(), Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
                        string strEconomicZone = item.EconomicZone == null ? "" : item.EconomicZone;
                        bookDocument.Add(new Field("EconomicZone", strEconomicZone.Trim().ToLower(), Field.Store.YES, Field.Index.NOT_ANALYZED, Field.TermVector.NO));
                        bookDocument.Add(new Field("Address", item.Address.Trim(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
                        indexWriter.AddDocument(bookDocument);
                    }

                    indexWriter.Optimize();
                    indexWriter.Commit();
                }
            }            
        }

        public static List<AddressSearchItem> Search(int customerid, int cusparnerid, string economic, string key, int recordFirst, int recordLast, ref int totalRecords)
        {
            string path = HttpContext.Current.Server.MapPath("/" + FolderUpload.Address);

            List<AddressSearchItem> result = new List<AddressSearchItem>();

            var searcher = new IndexSearcher(IndexReader.Open(Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(path)), true));
            BooleanQuery searchQuery = new BooleanQuery();
            //var queryKey = new QueryParser(Lucene.Net.Util.Version.LUCENE_29, "CustomerID", new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29)).Parse(customerid.ToString());
            //searchQuery.Add(queryKey, Occur.MUST);
            //queryKey = new QueryParser(Lucene.Net.Util.Version.LUCENE_29, "CUSParnerID", new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29)).Parse(cusparnerid.ToString());
            //searchQuery.Add(queryKey, Occur.MUST);
            TermQuery termQuery = new TermQuery(new Term("CustomerID", customerid.ToString()));
            searchQuery.Add(termQuery, Occur.MUST);
            termQuery = new TermQuery(new Term("CUSPartnerID", cusparnerid.ToString()));
            searchQuery.Add(termQuery, Occur.MUST);
            string strEconomic = economic == null ? "" : economic;
            termQuery = new TermQuery(new Term("EconomicZone", strEconomic.Trim().ToLower()));
            searchQuery.Add(termQuery, Occur.MUST);
            //var keyQuery = new QueryParser(Lucene.Net.Util.Version.LUCENE_29, key.Trim(), new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29));
            //searchQuery.Add(keyQuery, Occur.SHOULD);
            Query keyQuery = MultiFieldQueryParser.Parse(Lucene.Net.Util.Version.LUCENE_29, "\"" + key.Trim() + "\"", new[] { "Address" }, new[] { Occur.SHOULD }, new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29));
            searchQuery.Add(keyQuery, Occur.SHOULD);

            //var lstScore = searcher.Search(searchQuery, null, 100, Sort.).ScoreDocs;
            //for (int i = 0; i < lstScore.Length && i < recordLast; i++)
            //{
            //    var doc = searcher.Doc(lstScore[i].Doc);
            //    var obj = new AddressSearchItem();
            //    obj.CustomerID = Convert.ToInt32(doc.Get("CustomerID"));
            //    obj.CUSPartnerID = Convert.ToInt32(doc.Get("CUSPartnerID"));
            //    obj.CUSLocationID = Convert.ToInt32(doc.Get("CUSLocationID"));
            //    obj.LocationCode = doc.Get("LocationCode");
            //    obj.Address = doc.Get("Address");
            //    result.Add(obj);
            //}
            var hits = searcher.Search(searchQuery, null, 100);
            totalRecords = hits.TotalHits;

            recordLast = 20;

            for (int hitIndex = recordFirst; hitIndex < recordLast && hitIndex < totalRecords; hitIndex++)
            {
                var doc = searcher.Doc(hits.ScoreDocs[hitIndex].Doc);
                var obj = new AddressSearchItem();
                obj.CustomerID = Convert.ToInt32(doc.Get("CustomerID"));
                obj.CUSPartnerID = Convert.ToInt32(doc.Get("CUSPartnerID"));
                obj.CUSLocationID = Convert.ToInt32(doc.Get("CUSLocationID"));
                obj.LocationCode = doc.Get("LocationCode");
                obj.Address = doc.Get("Address");
                result.Add(obj);
            }
            return result;
        }
    }
}