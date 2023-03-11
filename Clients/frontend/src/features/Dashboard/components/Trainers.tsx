import { Card } from 'components/Cards/Card';
import { useGetPersonalTrainersQuery } from 'features/api/apiSlice';

export interface TrainersProps {
  numOfCards: number;
}

export const Trainers = ({ numOfCards }: TrainersProps) => {
  const { data: res, isLoading } = useGetPersonalTrainersQuery();

  if (isLoading) {
    return <p>loading...</p>;
  }

  return (
    <div className='flex items-center gap-12 ml-3'>
      {res?.data.map((trainer: any, i: number) => {
        if (i >= numOfCards) {
          console.log('aici');
          return (
            <p key={i} className='text-2xl'>
              ...
            </p>
          );
        }

        return (
          <Card
            key={i}
            name={trainer.name}
            title={trainer.certifications}
            image={trainer.imageUrl}
          />
        );
      })}
    </div>
  );
};
