using System;

namespace MSAbackend_phase2_cat
{
    public class CatVotes {
        //allow you to vote an image up or down ucan use
        //a sub_id to let each of your users vote and get
        //votes filtered by that sub_id.

        //subID is like an user id
        public string image_id { get; set; }
        public string sub_id { get; set; }
        public int value { get; set; }
    }

}
