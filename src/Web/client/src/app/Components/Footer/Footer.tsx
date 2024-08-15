/*
This React component renders the footer of the application. It displays the current year and a copyright notice 
for ByteShare, as well as a credit to Team 3. The footer is styled using an external CSS module.
*/

import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faGithub } from '@fortawesome/free-brands-svg-icons';
import styles from './FooterStyle.module.css';

const Footer: React.FC = () => {
    return (
        <footer className={styles.footer}>
            <div className={styles.text}>
                <p>&copy; {new Date().getFullYear()} MyGameStat. All rights reserved.<br />Brought to you by <strong>Team 3</strong></p>
            </div>
            <a href="https://github.com/240708-NET/MyGameStat/" className={styles.icon} target="_blank" rel="noopener noreferrer">
                <FontAwesomeIcon icon={faGithub} className="icon" />
            </a>

        </footer>
    );
};

export default Footer;

