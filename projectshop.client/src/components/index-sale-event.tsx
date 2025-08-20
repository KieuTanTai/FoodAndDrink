import React from "react";
//import "./SaleEventBlock.css";

export interface SaleEventItem {
    id: string | number;
    title: string;
    description?: string;
    image: string;
    link?: string;
    time?: string; // ví dụ: "18-08-2025 20:00"
}

interface SaleEventBlockProps {
    events: SaleEventItem[];
}

const SaleEventBlock: React.FC<SaleEventBlockProps> = ({ events }) => {
    return (
        <section className="sale-event-section">
            <div className="container-1200">
                <div className="sale-event-grid">
                    {events.map((event) => (
                        <div className="sale-event-item" key={event.id}>
                            <a href={event.link || "#"} className="sale-event-link">
                                <div className="sale-event-image-wrapper">
                                    <img
                                        src={event.image}
                                        alt={event.title}
                                        className="sale-event-image"
                                        loading="lazy"
                                    />
                                    {event.time && (
                                        <span className="sale-event-time">{event.time}</span>
                                    )}
                                </div>
                                <div className="sale-event-content">
                                    <h3 className="sale-event-title">{event.title}</h3>
                                    {event.description && (
                                        <p className="sale-event-desc">{event.description}</p>
                                    )}
                                </div>
                            </a>
                        </div>
                    ))}
                </div>
            </div>
        </section>
    );
};

export default SaleEventBlock;