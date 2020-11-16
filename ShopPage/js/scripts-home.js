// Set the date we're counting down to
  //var countDownDate = new Date("Jan 5, 2020 15:37:25").getTime();
  //var countDownDate = new Date(2020,3, 20, 15, 37, 25);
  var countDownDate = new Date(2020,05,30);

  // Update the count down every 1 second
  var x = setInterval(function() {
  
    // Get today's date and time
    var now = new Date().getTime();
      
    // Find the distance between now and the count down date
    var distance = countDownDate - now;
  
    // Time calculations for days, hours, minutes and seconds
    var Day = Math.floor(distance / (1000 * 60 * 60 * 24));
    var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
    var seconds = Math.floor((distance % (1000 * 60)) / 1000);
      
    // Output the result in an element with id="demo"
    document.getElementById("days").innerHTML = Day ;
    document.getElementById("hours").innerHTML = hours ;
    document.getElementById("mins").innerHTML = minutes ;
    document.getElementById("secs").innerHTML =  seconds ;
      
    // If the count down is over, write some text 
    if (distance < 0) {
      clearInterval(x);
      document.getElementById("days").innerHTML = '0' ;
    document.getElementById("hours").innerHTML = '0' ;
    document.getElementById("mins").innerHTML = '0' ;
    document.getElementById("secs").innerHTML = '0' ;
    }
  }, 1000);