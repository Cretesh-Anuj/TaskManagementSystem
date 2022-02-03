// user profile and sigout popup section
const userlistprofile = document.querySelector(
    ".main-container-image-section img"
  );
  const userlistlogoutsection = document.querySelector(
    ".main-container-logout-section"
  );
  const userlogoutsection = document.querySelector(
    ".main-container-logout-section-close-btn"
  );
  
  window.addEventListener("click", (e) => {
    const userlistprofileimgsection = document.querySelector(
      ".main-container-user-profile-img "
    );
    const userlistprofileimg = document.querySelector(
      ".main-container-user-profile-img img"
    );
    const userlistprofilename = document.querySelector(
      ".main-container-logout-section h3"
    );
    const userlistuseremail = document.querySelector(
      ".main-container-user-email"
    );
    const userlistprofilefooter = document.querySelector(
      ".main-container-logout-footer-section"
    );
    const userlistprofilefooterh3 = document.querySelector(
      ".main-container-logout-footer-section h3"
    );
    const userlistprofilelogoutbtn = document.querySelector(
      ".main-container-logout-btn"
    );
  
    if (e.target == userlistprofile) {
      userlistlogoutsection.classList.toggle("show");
    } else if (e.target == userlistprofileimgsection) {
      userlistlogoutsection.classList.add("show");
    } else if (e.target == userlistprofileimg) {
      userlistlogoutsection.classList.add("show");
    } else if (e.target == userlistprofilename) {
      userlistlogoutsection.classList.add("show");
    } else if (e.target == userlistuseremail) {
      userlistlogoutsection.classList.add("show");
    } else if (e.target == userlistprofilefooter) {
      userlistlogoutsection.classList.add("show");
    } else if (e.target == userlistprofilefooterh3) {
      userlistlogoutsection.classList.add("show");
    } else if (e.target == userlistlogoutsection) {
      userlistlogoutsection.classList.add("show");
    } else if (e.target == userlistprofilelogoutbtn) {
      userlistlogoutsection.classList.add("show");
    } else {
      userlistlogoutsection.classList.remove("show");
    }
  });
  
  // function for sigout
  function newDoc() {
    window.location.assign("http://127.0.0.1:5500/html/login.html");
  }
  function login() {
    window.location.assign("http://127.0.0.1:5500/html/Dashboard.html");
  }
  
  const sidebarbtn = document.querySelector(".main-container-sidebar-btn");
  const sidebarsection = document.querySelector(".sidebar");
  const userlist = document.querySelector(".main-container");
  const sidebarclosebtn = document.querySelector(".sidebar-close-btn");

  const navtext = document.querySelectorAll('.nav-text');
  const sidenavHeader = document.querySelectorAll(".sidenav-header");
  const subheader = document.querySelectorAll(".sidenav-subheader");
  const hamburgermenu = document.querySelector(".fa-bars");
  const dropdownIcon = document.querySelectorAll('.fa-chevron-right');

  console.log(subheader);

  
  sidebarbtn.addEventListener("click", (e) => {
    hamburgermenu.classList.toggle('down');

    dropdownIcon.forEach((e)=>{
      e.classList.remove('down');

    })
   
    // e.style.transform='rotate(90deg)';

    // this.toggleClass("down"); 
    sidebarsection.classList.toggle("showsidebar");
    sidebarsection.classList.toggle("hide");
    navtext.forEach((e)=>{
        e.classList.toggle("hide");
    })

    subheader.forEach((e)=>{
        e.style.display="none";
    }) 
    userlist.classList.toggle("active");
    sidebarbtn.classList.toggle("sidebarOpen");
  });
  
  // sidenav-container
  
  // console.log(sidenavsubHeader[0].innerHTML);

  

  var i=0;

  dropdownIcon.forEach((Event)=>{
   Event.addEventListener("click",(e)=>{
    e.target.classList.toggle('down');


       e.target.parentElement.parentElement.classList.toggle("active");
       var dropdownContent = e.target.parentElement.parentElement.nextElementSibling;
       if (dropdownContent.style.display === "block") {
         dropdownContent.style.display = "none";
       
       } else {
         dropdownContent.style.display = "block";
      
             
   
       }

   })


  });


  

  

  const currentLocation = location.href;
  // const sidenavsubHeader = document.querySelectorAll(".sidenav-subheader a");
  const sidenavsubHeader = document.querySelectorAll("a");
  const sidenavsubHeaderLength = sidenavsubHeader.length;
  
  for (i = 0; i < sidenavsubHeaderLength; i++) {
    if (sidenavsubHeader[i].href === currentLocation) {
      sidenavsubHeader[i].className = "active";
      sidenavsubHeader[i].parentElement.style.display = "block";
    }
  }