﻿using AutoMapper;
using CleanArchitecture.Application.Feature.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistance.Services
{
    public sealed class CarService : ICarService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CarService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateCarCommand request, CancellationToken cancellationToken)
        {
            
            Car car = _mapper.Map<Car>(request);

            await _context.Set<Car>().AddAsync(car, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
