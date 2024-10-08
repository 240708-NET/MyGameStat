import React from 'react';
import Slider from 'react-slick';
import 'slick-carousel/slick/slick.css';
import 'slick-carousel/slick/slick-theme.css';
import styles from './WishlistSlider.module.css';

interface WishlistItem {
    id: number;
    background_image: string;
    name: string;
    released: string;
    rating: number;
    esrb_rating?: {
        name: string;
    };
    platforms: {
        platform: {
            name: string;
        };
    }[];
    playtime: number;
    genres: {
        name: string;
    }[];
}

interface WishlistSliderProps {
    wishlistItems: WishlistItem[];
}

const WishlistSlider: React.FC<WishlistSliderProps> = ({ wishlistItems }) => {
    const settings = {
        dots: true,
        infinite: true,
        speed: 500,
        autoplay: true,
        autoplaySpeed: 5000, 
        slidesToShow: 3, 
        slidesToScroll: 3, 
    };

    return (
        <div className={styles.sliderContainer}>
            <Slider {...settings}>
                {wishlistItems.map(item => (
                    <div key={item.id} className={styles.slide}>
                        <img src={item.background_image} alt={item.name} className={styles.gameImage}  width={500} height={300}/>     
                        <h2 className={styles.gameName}>{item.name}</h2>   
                        <p>Release Date: {item.released}</p>
                        <p>Rating: {item.rating}</p>
                        <p>ESRB Rating: {item.esrb_rating ? item.esrb_rating.name : 'Not rated'}</p>
                        <p>Platforms: {item.platforms.map(platform => platform.platform.name).join(', ')}</p>
                        <p>Playtime: {item.playtime} hours</p>   
                        <p>Genres: {item.genres.map(genre => genre.name).join(', ')}</p>
                    </div>
                ))}
            </Slider>
        </div>
    );
};

export default WishlistSlider;
