import { motion, easeInOut } from "framer-motion";
import type UISaleEventSlideItemProps from "../../ui-props/sale-events/SaleEventItemProps";

/**
 * Slide effect: Slide luôn sang trái khi chuyển slide, không cần direction.
 */
const slideVariants = {
  initial: {
    opacity: 0,
    x: 6, // Slide từ phải vào
  },
  animate: {
    opacity: 1,
    x: 0,  // Ở giữa
    transition: { duration: 0.5, ease: easeInOut }
  },
  exit: {
    opacity: 0,
    x: -6, // Slide ra trái
    transition: { duration: 0.5, ease: easeInOut }
  }
};

export default function SaleEventSlideItem({ image, title, time, description }: UISaleEventSlideItemProps) {
  return (
    <motion.div
      variants={slideVariants}
      initial="initial"
      animate="animate"
      exit="exit"
      className="absolute inset-0"
      style={{ pointerEvents: "none" }}
    >
      <img
        src={image}
        alt={title}
        className="object-cover w-full h-full"
        loading="lazy"
      />
      <div className="absolute inset-0 bg-black/20 flex flex-col justify-end p-6">
        <h3 className="text-white text-2xl font-bold">{title}</h3>
        <p className="text-white text-sm">{description}</p>
        {time && (
          <span className="text-yellow-300 text-xs mt-2">{time}</span>
        )}
      </div>
    </motion.div>
  );
}