<h2>C# LIVE PROJECT</h2>
After completing the C#/.NET Framework bootcamp at The Tech Academy there is a 2 week live project where I worked with other students as an Agile development team.
We worked on a full scale MVC Web Application for a Theatre company in a legacy codebase using Azure DevOps.  Here we fixed bugs, cleaned up code and added 
new features and functionality. This was a great experience to learn the day to day duties of a software developer and learning how to come up with solutions 
to create a great product.
<br>
Below are some descriptions of stories I worked on accompanied by the code created to solve the stories.
<br>
<h4>1. RENTAL PAGE AND ADMIN NAVIGATION</h4>
This story required a new Rental screen and a navigation link under the Admin menu to show All Rentals for the theatre.  I scaffolded a new RentalController.cs 
and associated CRUD views. Then updated the shared _Layout.cshtml file to add a new ALL RENTALS navigation link to the ADMIN menu.

<h5>RENTAL CONTROLLER (RentalController.cs)</h5>
namespace TheatreCMS.Models
{
    public class Rental
    {
        [Key]
        public int RentalId { get; set; }

        [Display(Name = "Rental Name")]
        public string RentalName { get; set; }
        [Display(Name = "Rental Cost")]
        public int RentalCost { get; set; }
        public ICollection<RentalHistory> RentalHistories { get; set; } // changed string property to List<RentalHistory> and created the class for it
        [Display(Name = "Flaws And Damages")]
        public string FlawsAndDamages { get; set; }

        public int? RentalRequestId { get; set; }
        public RentalRequest RentalRequest { get; set; }
    }

    public class RentalEquipment : Rental
    {
        [Display(Name = "Choking Hazard")]
        public bool ChokingHazard { get; set; }
        [Display(Name = "Suffocation Hazard")]
        public bool SuffocationHazard { get; set; }
        [Display(Name = "Purchase Price")]
        public int PurchasePrice { get; set; }
    }

    public class RentalRoom : Rental
    {
        [Display(Name = "Room Number")]
        public int RoomNumber { get; set; }
        [Display(Name = "Square Footage")]
        public int SquareFootage { get; set; }
        [Display(Name = "Maximum Occupancy")]
        public int MaxOccupancy { get; set; }
    }
}

<h5>SHARED LAYOUT (_Layout.cshtml)</h5>                          
<p>class="dropdown-submenu" <br>
class="dropdown-item" href="@Url.Action("Index", "Rental")" All Rentals </p>
<br>

<h4>2. RENTAL REQUEST CRUD </h4>
This story required a new Rental Request screen and associated CRUD views to be scaffolded and all screens tested to verify functionality. While unit testing the new pages 
to ensure all were in working order, it was found that you couldn't create a new Rental Request record. There was no error message being returned so 
I added a Try Catch that would detail out the error and was able to find that there was an association to an existing RentalSurvey page that wasn't allowing a Rental Request 
record to be added. 

<h5>TRY CATCH CODE </h5>
try  
    {  
         
    }  
    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)  
    {  
        Exception raise = dbEx;  
        foreach (var validationErrors in dbEx.EntityValidationErrors)  
        {  
            foreach (var validationError in validationErrors.ValidationErrors)  
            {  
                string message = string.Format("{0}:{1}",  
                    validationErrors.Entry.Entity.ToString(),  
                    validationError.ErrorMessage);  
                // raise a new exception nesting  
                // the current instance as InnerException  
                raise = new InvalidOperationException(message, raise);  
            }  
        }  
        throw raise;  
    }  


<h5>RENTAL REQUEST CONTROLLER (RentalRequestController.cs) </h5>

    public class RentalRequestController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RentalRequest
        public ActionResult Index()
        {
            return View(db.RentalRequests.ToList());
        }

        // GET: RentalRequest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalRequest rentalRequest = db.RentalRequests.Find(id);
            if (rentalRequest == null)
            {
                return HttpNotFound();
            }
            return View(rentalRequest);
        }

        // GET: RentalRequest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RentalRequest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RentalRequestId,ContactPerson,Company,RequestedTime,StartTime,EndTime,ProjectInfo,RentalCode,Accepted,ContractSigned")] RentalRequest rentalRequest)
        {
            if (ModelState.IsValid)
            {
                db.RentalRequests.Add(rentalRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rentalRequest);
        }

        // GET: RentalRequest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalRequest rentalRequest = db.RentalRequests.Find(id);
            if (rentalRequest == null)
            {
                return HttpNotFound();
            }
            return View(rentalRequest);
        }

        // POST: RentalRequest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RentalRequestId,ContactPerson,Company,RequestedTime,StartTime,EndTime,ProjectInfo,RentalCode,Accepted,ContractSigned")] RentalRequest rentalRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rentalRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rentalRequest);
        }

        // GET: RentalRequest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalRequest rentalRequest = db.RentalRequests.Find(id);
            if (rentalRequest == null)
            {
                return HttpNotFound();
            }
            return View(rentalRequest);
        }

        // POST: RentalRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RentalRequest rentalRequest = db.RentalRequests.Find(id);
            db.RentalRequests.Remove(rentalRequest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }


<h4>3. UPDATE CAST MEMBER PAGE LAYOUT AND FIX ASPECT RATIO ON UPLOADED FILES </h4>
This story required a fix the the existing Cast Member screen to move fields around in a different order as well as ensure all fields lined 
up properly during a screen resize. This also needed to fix images, as currently they were being stretched out upon uploading.  To resolve these issues I modified the
CastMembers Create.cshtml file to rearrange the input fields and labels to the requested order. Lable and input field sizes needed to be adjusted in order to accommodate 
a screen resize.  The Image issue was resolved by removing the height attribute on the IMG line of code, leaving just the Width attribute.  

<h5>CAST MEMBER CREATE VIEW </h5>

        <div class="inputBox2">
            @Html.ValidationSummary(true, "", new { @class = "text-danger" })
            <div class="form-group">
                @Html.LabelFor(model => model.Name, htmlAttributes: new { @class = "control-label col-md-4 inputLabel" })
                <div class="col-md-10 formBox">
                    @Html.EditorFor(model => model.Name, new { htmlAttributes = new { @class = "form-control" } })
                    @Html.ValidationMessageFor(model => model.Name, "", new { @class = "text-dark" })
                </div>
            </div>
            <div class="form-group">
                <label class="inputLabel col-md-4"> &nbsp; &nbsp;Username</label>
                <div class="col-md-10 formBox">
                    @Html.DropDownList("dbUsers", (IEnumerable<SelectListItem>)ViewData["dbUsers"], "N/A", htmlAttributes: new { @class = "form-control" })
                    @Html.ValidationMessageFor(model => model.CastMemberPersonID, "", new { @class = "text-dark" })
                </div>
            </div>
            <div class="form-group">
                @Html.LabelFor(model => model.YearJoined, "Year Joined", htmlAttributes: new { @class = "control-label col-md-4 inputLabel" })
                <div class="col-md-10 formBox">
                    @Html.EditorFor(model => model.YearJoined, new { htmlAttributes = new { @class = "form-control" } })
                    @Html.ValidationMessageFor(model => model.YearJoined, "", new { @class = "text-danger" })
                </div>
            </div>

            <div class="form-group">
                @Html.LabelFor(model => model.MainRole, "Main Role", htmlAttributes: new { @class = "control-label col-md-4 inputLabel" })
                <div class="col-md-10  formBox">
                    @Html.EnumDropDownListFor(model => model.MainRole, htmlAttributes: new { @class = "form-control" })
                    @Html.ValidationMessageFor(model => model.MainRole, "", new { @class = "text-danger" })
                </div>
            </div>

            <div class="form-group">
                @Html.LabelFor(model => model.Bio, htmlAttributes: new { @class = "control-label col-md-4 inputLabel" })
                <div class="col-md-10 formBox">
                    @Html.EditorFor(model => model.Bio, new { htmlAttributes = new { @class = "form-control row-3" } })
                    @Html.ValidationMessageFor(model => model.Bio, "", new { @class = "text-danger" })
                </div>
            </div>

            <div class="form-group">
                @Html.Label("Photo") @*Change label from PhotoId to Photo*@
                <div class="col-md-10 formBox">
                    @*Create a preview image when file is selected*@
                    <img id="img" alt="" width="100" height="100" />
                    <input type="file" name="file" class="fileSelect" onchange="document.getElementById('img').src = window.URL.createObjectURL(this.files[0])">
                    @Html.ValidationMessageFor(model => model.PhotoId, "", new { @class = "text-danger" })

                </div>
            </div>

            <!--===== CHECKBOXES - match column width for group container =====-->
            <div class="container col-md-10 text-center">
                <div class="row justify-content-center">
                    <!--=== AssociateArtist ===-->
                    <div class="form-group">
                        @Html.LabelFor(model => model.AssociateArtist, htmlAttributes: new { @class = "control-label inputLabel" })
                        <div class="focus-negate">
                            @Html.EditorFor(model => model.AssociateArtist, new { htmlAttributes = new { @class = "form-control" } })
                            @Html.ValidationMessageFor(model => model.AssociateArtist, "", new { @class = "text-danger" })
                        </div>
                    </div>
                    <!--=== EnsembleMember ===-->
                    <div class="mx-4 form-group">
                        @Html.LabelFor(model => model.EnsembleMember, htmlAttributes: new { @class = "control-label inputLabel" })
                        <div class="focus-negate">
                            @Html.EditorFor(model => model.EnsembleMember, new { htmlAttributes = new { @class = "form-control" } })
                            @Html.ValidationMessageFor(model => model.EnsembleMember, "", new { @class = "text-danger" })
                        </div>
                    </div>
                    <!--=== CurrentMember ===-->
                    <div class="form-group">
                        @Html.LabelFor(model => model.CurrentMember, htmlAttributes: new { @class = "control-label inputLabel" })
                        <div class="focus-negate">
                            @Html.EditorFor(model => model.CurrentMember, new { htmlAttributes = new { @class = "form-control" } })
                            @Html.ValidationMessageFor(model => model.CurrentMember, "", new { @class = "text-danger" })
                        </div>
                    </div>
                </div>
            </div>
            <!--===== YEARS =====-->
            <div class="form-group">
                @Html.LabelFor(model => model.CastYearLeft, htmlAttributes: new { @class = "control-label col-md-4 inputLabel" })
                <div class="col-md-10 formBox">
                    @Html.EditorFor(model => model.CastYearLeft, new { htmlAttributes = new { @class = "form-control" } })
                    @Html.ValidationMessageFor(model => model.CastYearLeft, "", new { @class = "text-danger" })
                </div>
            </div>

            <div class="form-group">
                @Html.LabelFor(model => model.DebutYear, htmlAttributes: new { @class = "control-label col-md-4 inputLabel" })
                <div class="col-md-10 formBox">
                    @Html.EditorFor(model => model.DebutYear, new { htmlAttributes = new { @class = "form-control" } })
                    @Html.ValidationMessageFor(model => model.DebutYear, "", new { @class = "text-danger" })
                </div>
            </div>

            <!--===== SUBMIT =====-->
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10 formBox">
                    <input type="submit" value="Create" class="submitButton2" />
                </div>
            </div>
        </div>
    </div>
</div>
}

<div>
    @Html.ActionLink("Back to List", "Index")
</div>


