using Microsoft.AspNetCore.Mvc.Rendering;
using PPG_projekt.Models.AdminViewModels;
using PPG_projekt.Models.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPG_projekt.Services
{
    public interface IDictionaryService
    {

        List<SelectListItem> GetDictionaryObjectList(string name);
        List<SelectListItem> GetDictionariestList();
        IEnumerable<DictionaryObject> GetDictionariesObject(int i);
        IEnumerable<DictionaryObject> GetDeactiveDictionariesObject(int i);
        IEnumerable<Dict> GetDictionaries();
        IEnumerable<DictionaryObject> SearchObject(int i, string s);
        IEnumerable<DictionaryObject> GetDictionaryObject();
        IEnumerable<DictionarySubObject> GetDictionariesSubObject(int i);
        IEnumerable<DictionarySubObject> GetDeactivateDictionarySubObjects(int i);

        DictionaryObject DictionaryObjEdit(DictionaryObjEdit obj);
        DictionaryObject GetDictionaryObject(int i);

        DictionarySubObject GetDictionarySubObject(int i);
        DictionarySubObject DictionarySubObjEdit(DictionarySubObjEdit obj);

        Dict GetDictionary(int i);

        bool AddDictionary(string name);
        bool DeleteDictionary(int ID);
        bool AddDictionaryObject(int i, string name);
        bool DeleteDictionaryObject(int ID);
        bool ActiveDictionaryObject(int ID);
        bool AddDictionarySubObject(int i, string name);
        bool DeleteDictionarySubObject(int ID);
        bool ActiveDictionarySubObject(int ID);
    }
}
