'use client';

import React, { useState } from 'react';

import styles from './LoginStyle.module.css';

export default function Login() {
    const[user, setUser] = useState("");
    const[pass, setPass] = useState("");

    const[isLoggedIn, setIsLoggedIn] = useState(false);
    const[isActive, setIsActive] = useState(false);
    const[userError, setUserError] = useState(false);

    const toggleMenu = () => {
        setIsActive(!isActive);
        setUserError(false);
    }

    const toggleLogin = () => {
        setIsLoggedIn(!isLoggedIn);
        setUser("");
        setUserError(false);
    }

    const loginUser = async (e: { preventDefault: () => void; }) => {   
        e.preventDefault();

        //  TEST CODE    
        setIsLoggedIn(true);

        if (user == "") {
            setUser("{NV}");
        }

        //setUserError(true);
        //  TEST CODE ENDS

        //  Ensure user error message is inactive
        //setUserError(false);

        /*
        try {
            let response = await fetch(``);
            if (response.ok) {
                const text = await response.text();
                const data = text ? JSON.parse(text) : {}; 
    
                if (data != null && data.name === user.name) {
                    // Welcome user and log them in
                    //setIsLoggedIn(true);
                }
    
                else {
                    //  Display error message
                    //setUserError(true);
                }
            }

            else {
                console.error('Error occured while logging in', response.statusText);
            }
        }
        catch(error) {
            console.error('Error occured while logging in', error);
        }
        */
    };

    //  User is logged in
    if (isLoggedIn == true) {
        return <div className={`${styles.loginContainer}`}>
            <button onClick={toggleLogin} className={`${styles.loginButton}`}>{user}</button>
        </div>
    }

    else {
        if (isActive == false) {
            return <div className={`${styles.loginContainer}`}>
                <button onClick={toggleMenu} className={`${styles.loginButton}`}>Login | Sign up</button>
            </div>
        }
        else {
            return <div className={`${styles.loginContainer}`}>
                <div className={`${styles.loginPortal}`}>
                    <button onClick={toggleMenu} className={`${styles.portalButton}`}>X</button>

                    <form onSubmit={loginUser} className={`${styles.portalForm}`}>
                        <label>Username<br /><input type="text" name="uname" onChange={(e) => setUser(e.target.value)}></input></label><br /><br />
                        <label>Password<br /><input type="password" name="upass" onChange={(e) => setPass(e.target.value)}></input></label><br /><br />
                        <input type="submit" value="Login" id={`${styles.formSubmit}`}></input>
                    </form>

                    {userError == true && (
                        <p id={`${styles.errorMessage}`}>User doesn't exist!</p>
                    )}

                    <br />

                    <div className={`${styles.portalCreate}`}>
                        <p>Don't have an account?</p>
                        <button>Create an account</button>
                    </div>
                </div>
            </div>
        }
    }
}