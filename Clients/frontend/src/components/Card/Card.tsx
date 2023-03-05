import { useTranslation } from 'react-i18next';

import { Button } from 'components/Button';

interface CardProps {
  name: string;
  title: string;
  image: string;
}

export const Card = ({ name, title, image }: CardProps) => {
  const { t } = useTranslation();

  return (
    <div className={`h-[260px] w-[200px] flex flex-col rounded-lg shadow-xl relative border`}>
      <div className='basis-3/5'>
        <p className='mt-2 text-base font-bold text-center'>{name}</p>
        <p className='text-sm text-center'>{title}</p>
      </div>
      <div className='flex items-end justify-center mb-4 basis-2/5'>
        <Button text={t('button.viewProfile')} />
      </div>
      <div>
        <img
          className='absolute h-[200px] z-[-1] right-0 bottom-0'
          src={`http://localhost:5000/${image}`}
        />
      </div>
    </div>
  );
};
