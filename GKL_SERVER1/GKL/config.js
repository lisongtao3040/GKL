var PUB_YXLD_URL = "";
/*
var YXLD_URLS = {
    922: "http://10.160.204.152:5001/api/code",
    983: "http://10.162.201.93:5001/api/code"
}*/

//SRM1211A: "http://101.161.199.10:5001/api/code"
//SRM1211A: "http://10.160.204.180:5001/api/code?"
//SRM1341A: "http://10.162.207.144:5001/api/code",
var YXLD_URLS = {
    922: "http://101.160.204.152:5001/api/code",
    983: "http://101.161.199.10:5001/api/code",   
    SRM1223A: "http://10.160.204.135:5001/api/code",
    SRM1211A: "http://10.160.204.63:5001/api/code",
    SRM1341A: "http://10.162.207.144:5001/api/code",
   

}

//10.160.204.63    1号机
//10.162.207.144   13号机

function GetYXLD_URL(line_cd) {
    try {
        if (YXLD_URLS.hasOwnProperty(line_cd)) {
            return YXLD_URLS[line_cd];
        } else {
            return YXLD_URLS["922"];
        }

    }catch(e){
        return YXLD_URLS["922"];
    }

}