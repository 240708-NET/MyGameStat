import Image from "next/image";
import styles from "./page.module.css";

export default function Home() {


  return (
    <div className={styles.container}>
       <div className={styles.logoContainer}>
        <Image src="/images/logfinal.png" alt="Logo" width={120} height={100} />
      </div>
      <div className={styles.Content}>
        <h1 >Welcome To MyGameStat!</h1>

      </div>
      <h2 className={styles.sectionTitle}>Popular Games</h2>
      <div className={styles.cardsContainer}>

        <div className={styles.card}>

          <Image src="/images/image2.jpeg" alt="Card 1" width={300} height={200} />
          <div className={styles.cardFooter}>Burnout Paradise Remastered</div>
        </div>
        <div className={styles.card}>
          <Image src="/images/image1.png" alt="Card 2" width={300} height={200} />
          <div className={styles.cardFooter}>Call of Duty Modern Warfare 3 </div>
        </div>
        <div className={styles.card}>
          <Image src="/images/image3.jpeg" alt="Card 3" width={300} height={200} />
          <div className={styles.cardFooter}>Crime Boss: Rockay City</div>
        </div>
      </div>

      <div className={styles.cardsContainer}>
        <div className={styles.card}>

          <Image src="/images/image4.jpeg" alt="Card 1" width={300} height={200} />
          <div className={styles.cardFooter}>Need for Speed Unbound</div>
        </div>
        <div className={styles.card}>
          <Image src="/images/image5.jpeg" alt="Card 2" width={300} height={200} />
          <div className={styles.cardFooter}>EA SPORTS™ Madden NFL 25</div>
        </div>
        <div className={styles.card}>
          <Image src="/images/image6.jpeg" alt="Card 3" width={300} height={200} />
          <div className={styles.cardFooter}>SMITE 2</div>
        </div>

      </div>
    </div>
  );

}
