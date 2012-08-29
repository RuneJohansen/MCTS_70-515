function ShowHit(robotNo, weapon) {
    otherRobotNo = 1;
    if (robotNo == 1)
        otherRobotNo = 2;

    $("#imgWeapon").css("opacity", ".1");
    $("#imgWeapon").show();

    from = $(".imgR" + robotNo + ":first");
    to = $(".imgR" + otherRobotNo + ":first");
    console.log(from.position().top);
    console.log(to.position().left);
    /*
    var fromTop = $(".imgR" + robotNo + ":first).top;
    var toPos = $(".imgR" + otherRobotNo).position;
    $("#imgWeapon").css("top", fromTop);
    $("#imgWeapon").css("left", toPos.left);
    $("#imgWeapon").animate({"opacity":"0.5"}, 1000).animate({"top":"+=50"}, 3000);
    */
}

function ShowMiss(shootingRobot, weapon) {
    
}