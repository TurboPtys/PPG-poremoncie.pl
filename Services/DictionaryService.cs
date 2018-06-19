using PPG_projekt.Data;
using PPG_projekt.Models.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using PPG_projekt.Models.AdminViewModels;

namespace PPG_projekt.Services
{
    public class DictionaryService:IDictionaryService
    { 

    private readonly ApplicationDbContext _context;
    public DictionaryService(ApplicationDbContext context)
    {
        _context = context;
    }

        public IEnumerable<DictionaryObject> SearchObject(int i, string s)
        {

            var items = _context.DictionaryObjects.Where(m => m.Deactivate == false && m.DictionaryId == i && m.DictionaryObjectName.Contains(s)).ToArray();
            return items;
        }

        public IEnumerable<Dict> GetDictionaries()
        {
            var items =_context.Dictionaries.Where(m=>m.Deactivate==false).ToArray();
            return items;
        }

        public IEnumerable<DictionaryObject> GetDictionaryObject()
        {
            var items = _context.DictionaryObjects.Where(m => m.Deactivate == false && m.SubObj==true).ToArray();
            return items;
        }

        public IEnumerable<DictionarySubObject> GetDictionariesSubObject(int i)
        {
            var items = _context.DictionarySubObjects.Where(m => m.DictionaryObjectId == i && m.Deactivate == false).ToArray();
            return items;
        }

        public IEnumerable<DictionaryObject> GetDictionariesObject(int i)
        {
            var items = _context.DictionaryObjects.Where(m=>m.DictionaryId==i && m.Deactivate==false).ToArray();
            return items;
        }

        public DictionaryObject GetDictionaryObject(int i)
        {
            return _context.DictionaryObjects.Where(m => m.DictionaryObjectId == i).FirstOrDefault();
        }

        public IEnumerable<DictionaryObject> GetDeactiveDictionariesObject(int i)
        {
            var items = _context.DictionaryObjects.Where(m => m.DictionaryId == i && m.Deactivate == true).ToArray();
            return items;
        }

        public IEnumerable<DictionarySubObject>GetDictionarySubObjects(int i)
        {
            var items = _context.DictionarySubObjects.Where(m => m.DictionaryObjectId == i && m.Deactivate == false).ToArray();
            return items;
        }

        public IEnumerable<DictionarySubObject> GetDeactivateDictionarySubObjects(int i)
        {
            var items = _context.DictionarySubObjects.Where(m => m.DictionaryObjectId == i && m.Deactivate == true).ToArray();
            return items;
        }

        public List<SelectListItem> GetDictionaryObjectList(string name)
        {
            List<DictionaryObject> list = new List<DictionaryObject>();
            var dictionary = _context.Dictionaries.Where(m => m.DictionaryName.Equals(name)).FirstOrDefault();
            list = _context.DictionaryObjects.Where(m => m.DictionaryId == dictionary.DictionaryId && m.Deactivate == false).ToList();
            var listItems = list.Select(x => new SelectListItem { Value = x.DictionaryObjectName, Text = x.DictionaryObjectName }).ToList();
            return listItems;
            
        }

        public List<SelectListItem> GetDictionariestList()
        {
            List<Dict> list = new List<Dict>();
            
            list = _context.Dictionaries.Where(m => m.Deactivate == false).ToList();
            var listItems = list.Select(x => new SelectListItem { Value = x.DictionaryId.ToString(), Text = x.DictionaryName}).ToList();
            return listItems;

        }

        public DictionaryObject DictionaryObjEdit(DictionaryObjEdit obj)
        {
            var model = _context.DictionaryObjects.Where(m => m.DictionaryObjectId == obj.DictionaryObjID).FirstOrDefault();
            _context.Entry(model).Property(a => a.SubObj).CurrentValue = obj.SubObj;
            _context.Entry(model).Property(a => a.DictionaryId).CurrentValue = Int32.Parse(obj.DictionaryID);
            _context.Entry(model).Property(a => a.DictionaryObjectName).CurrentValue = obj.Name;
            _context.SaveChanges();

            return model;

        }

        

        public bool AddDictionary(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return false;
            }

            var dic = new Dict { DictionaryName=name,Deactivate=false };

            _context.Dictionaries.Add(dic);
            var saveResult = _context.SaveChanges();

            if (saveResult != null)
            {
                return true;
            }
            return false;

        }

        public bool DeleteDictionary(int ID)
        {
            var dict =  _context.Dictionaries.Where(m => m.DictionaryId ==ID ).FirstOrDefault();
            _context.Entry(dict).Property(a => a.Deactivate).CurrentValue = true;
            _context.SaveChanges();

            return dict.Deactivate;

        }

        public bool AddDictionaryObject(int i, string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return false;
            }

            var dicObj = new DictionaryObject { DictionaryId=i,DictionaryObjectName=name,Deactivate=false };

            _context.DictionaryObjects.Add(dicObj);
            var saveResult = _context.SaveChanges();

            if (saveResult != null)
            {
                return true;
            }
            return false;


        }

        public bool DeleteDictionaryObject(int ID)
        {
            var dict = _context.DictionaryObjects.Where(m => m.DictionaryObjectId == ID).FirstOrDefault();
            _context.Entry(dict).Property(a => a.Deactivate).CurrentValue = true;
            _context.SaveChanges();

            return dict.Deactivate;
        }

        public bool ActiveDictionaryObject(int ID)
        {
            var dict = _context.DictionaryObjects.Where(m => m.DictionaryObjectId == ID).FirstOrDefault();
            _context.Entry(dict).Property(a => a.Deactivate).CurrentValue = false;
            _context.SaveChanges();

            return !dict.Deactivate;
        }

        public bool AddDictionarySubObject(int i, string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return false;
            }

            var dicObj = new DictionarySubObject { Deactivate=false,DictionaryObjectId=i,DictionarySubObjectName=name };

            _context.DictionarySubObjects.Add(dicObj);
            var saveResult = _context.SaveChanges();

            return true;
        }

        public bool DeleteDictionarySubObject(int ID)
        {
            var dict = _context.DictionarySubObjects.Where(m => m.DictionarySubObjectId == ID).FirstOrDefault();
            _context.Entry(dict).Property(a => a.Deactivate).CurrentValue = true;
            _context.SaveChanges();

            return dict.Deactivate;
        }

        public bool ActiveDictionarySubObject(int ID)
        {
            var dict = _context.DictionarySubObjects.Where(m => m.DictionarySubObjectId == ID).FirstOrDefault();
            _context.Entry(dict).Property(a => a.Deactivate).CurrentValue = false;
            _context.SaveChanges();

            return !dict.Deactivate;
        }

        public DictionarySubObject GetDictionarySubObject(int i)
        {
            return _context.DictionarySubObjects.Where(m => m.DictionarySubObjectId == i).FirstOrDefault();
        }

        public DictionarySubObject DictionarySubObjEdit(DictionarySubObjEdit obj)
        {
            var Dic = _context.DictionaryObjects.Where(m=>m.DictionaryObjectName.Equals(obj.DictionaryObjID)).First();
            var model = _context.DictionarySubObjects.Where(m => m.DictionarySubObjectId == obj.DictionarySubObjID).FirstOrDefault();
            _context.Entry(model).Property(a => a.DictionaryObjectId).CurrentValue =Dic.DictionaryObjectId;
            _context.Entry(model).Property(a => a.DictionarySubObjectName).CurrentValue = obj.Name;
            _context.SaveChanges();

            return model;

        }

        public Dict GetDictionary(int i)
        {
            var model = _context.Dictionaries.Where(m => m.DictionaryId == i).First();
            return model;
        }
    }
}
