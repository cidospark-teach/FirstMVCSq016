// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


/**
 
var url = 'Test/SaveReportDetail';
var username =  'example' ;

fetch(url, {
    method: 'POST', // or 'PUT'
    body: JSON.stringify(username), 
    headers: {
        'Accept': 'application/json; charset=utf-8',
        'Content-Type': 'application/json;charset=UTF-8'
    }
}).then(res => res.json())
    .then(response => console.log('Success:', JSON.stringify(response)))
    .catch(error => console.error('Error:', error));

Action:
[HttpPost]
public JsonResult SaveReportDetail([FromBody]string username)


 */