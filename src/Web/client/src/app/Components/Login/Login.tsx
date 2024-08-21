'use client';

import React, { useState } from 'react';

import styles from './LoginStyle.module.css';

export default function Login() {
    const[user, setUser] = useState("");
    const[pass, setPass] = useState("");
    const[hasLength, setHasLength] = useState(false);
    const[hasLower, setHasLower] = useState(false);
    const[hasUpper, setHasUpper] = useState(false);
    const[hasNumber, setHasNumber] = useState(false);
    const[hasSpecial, setHasSpecial] = useState(false);

    const[status, setStatus] = useState("inactive");
    const[loggedIn, setLoggedIn] = useState(false);
    const[response, setResponse] = useState("");

    const updatePass = (e: string) => {
        let word = e;

        if (word == "") {
            setHasLength(false);
            setHasLower(false);
            setHasUpper(false);
            setHasNumber(false);
            setHasSpecial(false);
        }

        else {
            setHasLength(word.length >= 8);
            setHasLower(word != word.toUpperCase());
            setHasUpper(word != word.toLowerCase());

            var check = /\d+/g;
            setHasNumber(check.test(word));

            check = /[ `!@#$%^&*]/;
            setHasSpecial(check.test(word));
        }

        setPass(e);
    }

    const toggleActive = () => {
        setStatus((status == "active") ? "inactive" : "active");
        setResponse("");
    }

    const toggleCreate = () => {
        setStatus((status == "active") ? "create" : "inactive");
        setResponse("");
    }

    const loginUser = async (e: { preventDefault: () => void; }) => {
        e.preventDefault();

        if (user != "" && pass != "") {
            try {
                setLoggedIn(true);
                setStatus("inactive");

                /*
                var response = await fetch('http://localhost:7904/login/', {
                    method: 'POST',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({
                        "email": user,
                        "password": pass,
                        "twoFactorCode": "",
                        "twoFactorRecoveryCode": "",
                    })
                })

                if (response.ok) {
                    setLoggedIn(true);
                    setStatus("inactive");
                }

                else {
                    setResponse("Invalid username or password");
                }
                */
            }

            catch(error) {
                setResponse("An error has occured");
                console.error(error);
            }
        }

        else {
            setResponse("Invalid username or password");
        }
    }

    const createUser = async (e: { preventDefault: () => void; }) => {
        e.preventDefault();

        if (user != "" && pass != "") {
            try {
                setLoggedIn(true);
                setStatus("inactive");
                
                /*
                var response = await fetch('http://localhost:7904/register', {
                    method: 'POST',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({
                        email: user,
                        password: pass,
                    })
                })

                if (response.ok) {
                    setLoggedIn(true);
                    setStatus("inactive");
                }

                else {
                    setResponse("Invalid username or password");
                }
                */


            }

            catch(error) {
                setResponse("An error has occured");
                console.error(error);
            }
        }

        else {
            setResponse("Invalid username or password");
        }
    }

    switch(status) {
        case "inactive":
            //  User is logged in already
            if (loggedIn) {
                return (<div className={`${styles.loginContainer}`}>
                    <button onClick={toggleActive} className={`${styles.loginButton}`}>Hello, {user}</button>
                </div>);
            }

            //  User isn't logged in
            else {
                return (<div className={`${styles.loginContainer}`}>
                    <button onClick={toggleActive} className={`${styles.loginButton}`}>Login | Sign up</button>
                </div>);
            }

        case "active":
            //  User is logged in already
            if (loggedIn) {
                return (<div className={`${styles.loginContainer}`}>
                    <div className={`${styles.loginPortal}`}>
                        <button onClick={toggleActive} className={`${styles.portalButton}`}>X</button>
                    </div>
                </div>);
            }

            //  User isn't logged in
            else {
                return (<div className={`${styles.loginContainer}`}>
                    <div className={`${styles.loginPortal}`}>
                        <button onClick={toggleActive} className={`${styles.portalButton}`}>X</button>
    
                        <br />
                        <form onSubmit={loginUser} className={`${styles.portalForm}`}>
                            <label>Username<br /><input type="text" name="uname" onChange={(e) => setUser(e.target.value)}></input></label><br /><br />
                            <label>Password<br /><input type="password" name="upass" onChange={(e) => setPass(e.target.value)}></input></label><br />
                            <input type="submit" value="Login" id={`${styles.formSubmit}`}></input>
                        </form>

                        {response != "" && (
                            <p className={`${styles.errorMessage}`}>{response}</p>
                        )}

                        <br />
                        <div className={`${styles.portalCreate}`}>
                            <p>Don't have an account?</p>
                            <button onClick={toggleCreate}>Create an account</button>
                        </div>
                    </div>
                </div>);
            }

        case "create":
            return (<div className={`${styles.loginContainer}`}>
                <div className={`${styles.loginPortal}`}>
                    <button onClick={toggleCreate} className={`${styles.portalButton}`}>X</button>

                    <br />
                    <form onSubmit={createUser} className={`${styles.portalForm}`}>
                        <label>Username<br /><input type="text" name="uname" onChange={(e) => setUser(e.target.value)}></input></label><br /><br />
                        <label>Password<br /><input type="password" name="upass" onInput={(e) => updatePass(e.currentTarget.value)}></input></label><br />
                        <label id={hasLength ? `${styles.formPass}` : `${styles.formFail}`}>At least 8 characters</label><br />
                        <label id={hasLower ? `${styles.formPass}` : `${styles.formFail}`}>At least 1 lowercase</label><br />
                        <label id={hasUpper ? `${styles.formPass}` : `${styles.formFail}`}>At least 1 uppercase</label><br />
                        <label id={hasNumber ? `${styles.formPass}` : `${styles.formFail}`}>At least 1 number</label><br />
                        <label id={hasSpecial ? `${styles.formPass}` : `${styles.formFail}`}>At least 1 special character</label><br /><br />
                        <input type="submit" value="Create User" id={`${styles.formSubmit}`}></input>
                    </form>
                </div>
            </div>);
    }
}