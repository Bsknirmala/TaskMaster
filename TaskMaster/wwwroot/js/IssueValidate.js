$(function() {
    var $issueForm = $("#myform");
    if($issueForm.length) {
        $issueForm.validate({
            rules: {
                description: {
                    required: true
                }
            },
            messages: {
                description: {
                    required: 'description is mandatory'
                }
            }
        })
    }

})