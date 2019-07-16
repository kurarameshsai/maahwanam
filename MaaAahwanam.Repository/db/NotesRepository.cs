﻿using MaaAahwanam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaAahwanam.Repository.db
{
   public class NotesRepository
    {
        readonly ApiContext _dbContext = new ApiContext();

        public List<Note> Getnote(long wishlist_id,long vendor_id)
        {
            return _dbContext.Notes.Where(n => n.wishlistId == wishlist_id && n.VendorId == vendor_id).ToList();
        }

        public Note AddNotes(Note note)
        {
            _dbContext.Notes.Add(note);
            _dbContext.SaveChanges();
            return note;
        }

        public collabratornotes addcollabratornote(collabratornotes cnotes)
        {
            _dbContext.collabratornotes.Add(cnotes);
            _dbContext.SaveChanges();
            return cnotes;
        }

        public collabratornotes UpdatecollabratorNotes(collabratornotes note, long notesId)
        {
            var Notedata = _dbContext.collabratornotes.SingleOrDefault(n => n.collabratornotesId == notesId);
            note.collabratornotesId = Notedata.collabratornotesId;
            note.wishlist_id = Notedata.wishlist_id;
            note.vendor_id = Notedata.vendor_id;
            note.AddedDate = Notedata.AddedDate;
            _dbContext.Entry(Notedata).CurrentValues.SetValues(note);
            _dbContext.SaveChanges();
            return note;
        }

        public int RemovecollabratorNotes(long notesId)
        {
            int i;
            var data = _dbContext.collabratornotes.Where(n => n.collabratornotesId == notesId).FirstOrDefault();
            if (data != null)
            {
                _dbContext.collabratornotes.Remove(data);
                _dbContext.SaveChanges();
                i = 1;
            }
            else
                i = 0;
            return i;
        }


        public Note UpdateNotes(Note note, long notesId)
        {
            var Notedata = _dbContext.Notes.SingleOrDefault(n => n.NotesId == notesId);
            note.NotesId = Notedata.NotesId;
            note.wishlistId = Notedata.wishlistId;
            note.wishlistItemId = Notedata.wishlistItemId;
            note.VendorId = Notedata.VendorId;
            note.AddedDate = Notedata.AddedDate;
            _dbContext.Entry(Notedata).CurrentValues.SetValues(note);
            _dbContext.SaveChanges();
            return note;
        }

        public int RemoveNotes(long notesId)
        {
            int i;
            var data = _dbContext.Notes.Where(n => n.NotesId == notesId).FirstOrDefault();
            if (data != null)
            {
                _dbContext.Notes.Remove(data);
                _dbContext.SaveChanges();
                i = 1;
            }
            else
                i = 0;
            return i;
        }

        public long GetcollabratorDetailsByEmail(string username,long userid)
        {
            var count = _dbContext.Collabrator.Where(m => m.Email == username && m.UserId == userid).FirstOrDefault();
            if (count != null)
                return count.Id;
            else
                return 0;
        }

        public Collabrator AddCollabrator(Collabrator collabrator)
        {
            _dbContext.Collabrator.Add(collabrator);
            _dbContext.SaveChanges();
            return collabrator;
        }

        public int RemoveCollabrator(long collabratorId)
        {
            var getdata = _dbContext.Collabrator.Where(m => m.Id == collabratorId).FirstOrDefault();
            _dbContext.Collabrator.Remove(getdata);
            return _dbContext.SaveChanges();
        }
    }
}
