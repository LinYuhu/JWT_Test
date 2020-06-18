<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="EFWebApi.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="./css/Default.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div v-cloak id="app">
            {{message}}
            <div>
                帳號：<input type="text" v-model="account"/><br />
                密碼：<input type="text" v-model="password"/><br />
                <button type="button" @click="doLogIn()">Log In</button><br />
              Payload：{{payload}}<br />
                <input type="text" v-model="payload"/>
            </div>
            <br /><br />
           <button type="button" @click="getUerName()">getUerName</button><br />
            Status：{{status}}<br />
            UserName：{{userName}}
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/vue/dist/vue.js"></script>
    <script src="https://unpkg.com/axios/dist/axios.min.js"></script>
    <script src="../js/sha1.js"></script>
    <script >
        var app = new Vue({
            el: '#app',
            data: {
                message: 'Hello Vue!',
                account: '535208',
                password: '123456',
                payload: '',
                userName: '',
                status: ''
            },
            methods: {
                doLogIn: function () {
                    const vm = this;
                    axios.post('../Auth/LogIn', {
                        Account: vm.account,
                        Password: sha1(vm.password).toUpperCase(),
                    })
                        .then(function (response) {
                            vm.payload = response.data.Payload;
                        })
                        .catch(function (error) {
                            vm.payload = '';
                            if (error.response.status == 401)
                                alert(error.response.data.Message);
                        });
                },
                getUerName: function () {
                    const vm = this;
                    const AuthStr = 'Bearer ' + vm.payload;
                    const URL = '../Test/GetConsignorName/' + vm.account;
                    axios.get(URL, { headers: { Authorization: AuthStr } })
                        .then(function (response) {
                            vm.status = response.status;
                            vm.userName = response.data;
                        })
                        .catch(function (error) {
                            vm.status = error.response.status;
                            if (error.response.status == 401)
                                vm.userName = error.response.data.Message;
                            else
                                vm.userName = error;
                        });
                }
            }
        })
    </script>
</body>
</html>
