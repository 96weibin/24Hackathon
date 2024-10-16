<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Summary.aspx.cs" Inherits="QuestionList.Summary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table width="500" border="1">
            <caption><h3>Question List</h3></caption>
            <tr>
                <th>Title</th>
                <th>Summary</th>
            </tr>
            <%
                List <QuestionSummary> data = getAllQuestion();
                foreach(QuestionSummary item in data)
            {
            %>
            <tr>
                <td><%=item.Title.ToString() %></td>
                <td><%=item.Summary.ToString() %></td>
            </tr>
            <%
            }
            %>
        </table>
    </form>
</body>
</html>
