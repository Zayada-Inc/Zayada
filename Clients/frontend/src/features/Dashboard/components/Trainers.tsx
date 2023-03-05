import { Card } from 'components/Card/Card';
import { useGetPersonalTrainersQuery } from 'features/api/apiSlice';

export interface TrainersProps {
  numOfCards: number;
}

export const Trainers = ({ numOfCards }: TrainersProps) => {
  const { data: res, isLoading } = useGetPersonalTrainersQuery();
  console.log(res?.data);
  if (isLoading) {
    return <p>loading...</p>;
  }

  return (
    <div className='flex gap-12 ml-3 border-2 items-center'>
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
